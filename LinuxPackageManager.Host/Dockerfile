FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["LinuxPackageManager.Host/LinuxPackageManager.Host.csproj", "LinuxPackageManager.Host/"]
COPY ["LinuxPackageManager.Api.Contracts/LinuxPackageManager.Api.Contracts.csproj", "LinuxPackageManager.Api.Contracts/"]
COPY ["LinuxPackageManager.Application/LinuxPackageManager.Application.csproj", "LinuxPackageManager.Application/"]
COPY ["LinuxPackageManager.Events/LinuxPackageManager.Events.csproj", "LinuxPackageManager.Events/"]
COPY ["LinuxPackageManager.Domain/LinuxPackageManager.Domain.csproj", "LinuxPackageManager.Domain/"]

RUN dotnet restore "LinuxPackageManager.Host/LinuxPackageManager.Host.csproj"

COPY . .

WORKDIR "/src/LinuxPackageManager.Host"
RUN dotnet build "LinuxPackageManager.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LinuxPackageManager.Host.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LinuxPackageManager.Host.dll"]