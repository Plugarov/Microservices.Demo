#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WorkshopManagementEventHandler/WorkshopManagementEventHandler.csproj", "WorkshopManagementEventHandler/"]
COPY ["Infrastructure.Messaging/Infrastructure.Messaging.csproj", "Infrastructure.Messaging/"]
RUN dotnet restore "WorkshopManagementEventHandler/WorkshopManagementEventHandler.csproj"
COPY . .
WORKDIR "/src/WorkshopManagementEventHandler"
RUN dotnet build "WorkshopManagementEventHandler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkshopManagementEventHandler.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "WorkshopManagementEventHandler.dll"]