# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 as build
WORKDIR /src

COPY ["PublishingService/PublishingService.csproj", "PublishingService/"]
RUN dotnet restore "PublishingService/PublishingService.csproj"

COPY . .
WORKDIR "/src/PublishingService"
RUN dotnet publish "PublishingService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/runtime:10.0 AS base
WORKDIR /app

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PublishingService.dll"]