#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["VehicleManagementAPI/VehicleManagementAPI.csproj", "VehicleManagementAPI/"]
COPY ["Infrastructure.Messaging/Infrastructure.Messaging.csproj", "Infrastructure.Messaging/"]
RUN dotnet restore "VehicleManagementAPI/VehicleManagementAPI.csproj"
COPY . .
WORKDIR "/src/VehicleManagementAPI"
RUN dotnet build "VehicleManagementAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VehicleManagementAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 4100/tcp
ENV ASPNETCORE_URLS http://*:4100

ENTRYPOINT ["dotnet", "VehicleManagementAPI.dll"]