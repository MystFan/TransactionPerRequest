FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TransactionPerRequest.Api/TransactionPerRequest.Api.csproj", "TransactionPerRequest.Api/"]
COPY ["TransactionPerRequest.Data/TransactionPerRequest.Data.csproj", "TransactionPerRequest.Data/"]
RUN dotnet restore "TransactionPerRequest.Api/TransactionPerRequest.Api.csproj"
COPY . .
WORKDIR "/src/TransactionPerRequest.Api"
RUN dotnet build "TransactionPerRequest.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TransactionPerRequest.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransactionPerRequest.Api.dll"]