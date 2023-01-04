#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Trade/Api/Trades.Api.csproj", "src/Services/Trade/Api/"]
COPY ["src/Services/Trade/Application/Trades.Application.csproj", "src/Services/Trade/Application/"]
COPY ["src/Services/Trade/Domain/Trades.Domain.csproj", "src/Services/Trade/Domain/"]
COPY ["src/Services/Trade/Infrastructure/Trades.Infrastructure.csproj", "src/Services/Trade/Infrastructure/"]
RUN dotnet restore "src/Services/Trade/Api/Trades.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Trade/Api"
RUN dotnet build "Trades.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Trades.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Trades.Api.dll"]