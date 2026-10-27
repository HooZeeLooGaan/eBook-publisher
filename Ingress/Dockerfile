# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Ingress/Ingress.csproj", "Ingress/"]
RUN dotnet restore "Ingress/Ingress.csproj"

COPY . .
WORKDIR "/src/Ingress"
RUN dotnet publish "Ingress.csproj" -c Release -o /app/publish /p:UseAppHost=false


# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet", "Ingress.dll" ]