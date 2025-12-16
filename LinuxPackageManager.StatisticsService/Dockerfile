FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["LinuxPackageManager.StatisticsService/LinuxPackageManager.StatisticsService.csproj", "LinuxPackageManager.StatisticsService/"]
COPY ["LinuxPackageManager.Events/LinuxPackageManager.Events.csproj", "LinuxPackageManager.Events/"]

RUN dotnet restore "LinuxPackageManager.StatisticsService/LinuxPackageManager.StatisticsService.csproj"

COPY . .
WORKDIR "/src/LinuxPackageManager.StatisticsService"
RUN dotnet build "LinuxPackageManager.StatisticsService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LinuxPackageManager.StatisticsService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LinuxPackageManager.StatisticsService.dll"]