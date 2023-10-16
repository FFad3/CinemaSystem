#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["CinemaSystem.Api/CinemaSystem.Api.csproj", "CinemaSystem.Api/"]
COPY ["CinemaSystem.Infrastructure/CinemaSystem.Infrastructure.csproj", "CinemaSystem.Infrastructure/"]
COPY ["CinemaSystem.Application/CinemaSystem.Application.csproj", "CinemaSystem.Application/"]
COPY ["CinemaSystem.Core/CinemaSystem.Core.csproj", "CinemaSystem.Core/"]
RUN dotnet restore "CinemaSystem.Api/CinemaSystem.Api.csproj"
COPY . .
WORKDIR "/src/CinemaSystem.Api"
RUN dotnet build "CinemaSystem.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CinemaSystem.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CinemaSystem.Api.dll"]