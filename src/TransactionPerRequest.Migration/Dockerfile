FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TransactionPerRequest.Migration/TransactionPerRequest.Migration.csproj", "TransactionPerRequest.Migration/"]
COPY ["TransactionPerRequest.Data/TransactionPerRequest.Data.csproj", "TransactionPerRequest.Data/"]
RUN dotnet restore "TransactionPerRequest.Migration/TransactionPerRequest.Migration.csproj"
COPY . .
WORKDIR "/src/TransactionPerRequest.Migration"
RUN dotnet build "TransactionPerRequest.Migration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TransactionPerRequest.Migration.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransactionPerRequest.Migration.dll"]