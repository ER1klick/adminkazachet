FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["LinuxPackageManager.AuditService/LinuxPackageManager.AuditService.csproj", "LinuxPackageManager.AuditService/"]
COPY ["LinuxPackageManager.Events/LinuxPackageManager.Events.csproj", "LinuxPackageManager.Events/"]

RUN dotnet restore "LinuxPackageManager.AuditService/LinuxPackageManager.AuditService.csproj"

COPY . .
WORKDIR "/src/LinuxPackageManager.AuditService"
RUN dotnet build "LinuxPackageManager.AuditService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LinuxPackageManager.AuditService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LinuxPackageManager.AuditService.dll"]