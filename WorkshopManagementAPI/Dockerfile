#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["WorkshopManagementAPI/WorkshopManagementAPI.csproj", "WorkshopManagementAPI/"]
COPY ["Infrastructure.Messaging/Infrastructure.Messaging.csproj", "Infrastructure.Messaging/"]
RUN dotnet restore "WorkshopManagementAPI/WorkshopManagementAPI.csproj"
COPY . .
WORKDIR "/src/WorkshopManagementAPI"
RUN dotnet build "WorkshopManagementAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WorkshopManagementAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 4200/tcp
ENV ASPNETCORE_URLS http://*:4200

ENTRYPOINT ["dotnet", "WorkshopManagementAPI.dll"]