#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/NotificationService/Api/Notifications.Api.csproj", "src/Services/NotificationService/Api/"]
COPY ["src/Services/NotificationService/Application/Notifications.Application.csproj", "src/Services/NotificationService/Application/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj", "src/BuildingBlocks/EventBus/EventBus.RabbitMQ/"]
COPY ["src/Services/NotificationService/Domain/Notifications.Domain.csproj", "src/Services/NotificationService/Domain/"]
COPY ["src/Services/NotificationService/Infrastructure/Notifications.Infrastructure.csproj", "src/Services/NotificationService/Infrastructure/"]
RUN dotnet restore "src/Services/NotificationService/Api/Notifications.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/NotificationService/Api"
RUN dotnet build "Notifications.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notifications.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notifications.Api.dll"]