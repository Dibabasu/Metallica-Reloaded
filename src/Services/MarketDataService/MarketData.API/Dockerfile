#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/MarketDataService/MarketData.API/MarketData.API.csproj", "src/Services/MarketDataService/MarketData.API/"]
RUN dotnet restore "src/Services/MarketDataService/MarketData.API/MarketData.API.csproj"
COPY . .
WORKDIR "/src/src/Services/MarketDataService/MarketData.API"
RUN dotnet build "MarketData.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarketData.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarketData.API.dll"]