FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копируем проекты
COPY ["LinuxPackageManager.AnalyticsService/LinuxPackageManager.AnalyticsService.csproj", "LinuxPackageManager.AnalyticsService/"]
COPY ["LinuxPackageManager.Api.Contracts/LinuxPackageManager.Api.Contracts.csproj", "LinuxPackageManager.Api.Contracts/"]

RUN dotnet restore "LinuxPackageManager.AnalyticsService/LinuxPackageManager.AnalyticsService.csproj"

COPY . .
WORKDIR "/src/LinuxPackageManager.AnalyticsService"
RUN dotnet build "LinuxPackageManager.AnalyticsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LinuxPackageManager.AnalyticsService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LinuxPackageManager.AnalyticsService.dll"]