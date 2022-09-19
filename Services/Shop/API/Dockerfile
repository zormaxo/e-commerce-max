#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Shop/API/Shop.API.csproj", "Services/Shop/API/"]
COPY ["Services/Shop/Application/Shop.Application.csproj", "Services/Shop/Application/"]
COPY ["Services/Shop/Infrastructure/Shop.Infrastructure.csproj", "Services/Shop/Infrastructure/"]
COPY ["Services/Shop/Core/Shop.Core.csproj", "Services/Shop/Core/"]
RUN dotnet restore "Services/Shop/API/Shop.API.csproj"
COPY . .
WORKDIR "/src/Services/Shop/API"
RUN dotnet build "Shop.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shop.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shop.API.dll"]