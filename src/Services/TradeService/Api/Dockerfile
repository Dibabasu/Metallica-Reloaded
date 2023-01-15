#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/TradeService/Api/Trades.Api.csproj", "src/Services/TradeService/Api/"]
COPY ["src/Services/TradeService/Application/Trades.Application.csproj", "src/Services/TradeService/Application/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj", "src/BuildingBlocks/EventBus/EventBus.RabbitMQ/"]
COPY ["src/Services/TradeService/Domain/Trades.Domain.csproj", "src/Services/TradeService/Domain/"]
COPY ["src/Services/TradeService/Infrastructure/Trades.Infrastructure.csproj", "src/Services/TradeService/Infrastructure/"]
RUN dotnet restore "src/Services/TradeService/Api/Trades.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/TradeService/Api"
RUN dotnet build "Trades.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Trades.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Trades.Api.dll"]