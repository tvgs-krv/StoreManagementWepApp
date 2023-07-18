FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["StockData.Api/StockData.Api.csproj", "StockData.Api/"]
RUN dotnet restore "StockData.Api/StockData.Api.csproj"
COPY . .
WORKDIR "/src/StockData.Api"
RUN dotnet build "StockData.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StockData.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockData.Api.dll"]