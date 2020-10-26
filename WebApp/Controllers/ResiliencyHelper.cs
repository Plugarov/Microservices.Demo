﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class ResiliencyHelper
    {
        private ILogger _logger;

        public ResiliencyHelper(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> ExecuteResilient(Func<Task<IActionResult>> action, IActionResult fallbackResult)
        {
            var retryPolicy = Policy
                .Handle<Exception>((ex) =>
                {
                    _logger.LogWarning($"Error occured during request-execution. Polly will retry. Exception: {ex.Message}");
                    return true;
                })
                .RetryAsync(5);

            var fallbackPolicy = Policy<IActionResult>
                .Handle<Exception>()
                .FallbackAsync(
                    fallbackResult,
                    (e, c) => Task.Run(() => _logger.LogError($"Error occured during request-execution. Polly will fallback. Exception: {e.Exception.ToString()}")));

            return await fallbackPolicy
                .WrapAsync(retryPolicy)
                .ExecuteAsync(action)
                .ConfigureAwait(false);
        }
    }
}
