#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

RUN apt-get update
RUN apt-get install -y docker.io

COPY Azurite/certs/azuriteCert.crt /usr/local/share/ca-certificates
RUN "update-ca-certificates"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Application/Cargowhale/Cargowhale.csproj", "Application/Cargowhale/"]
COPY ["Domain/CargoWhale.Domain.DbExport/Cargowhale.Domain.Sql.csproj", "Domain/CargoWhale.Domain.DbExport/"]
COPY ["Contracts/Cargowhale.Contracts.DbExport/Cargowhale.Contracts.DbExport.csproj", "Contracts/Cargowhale.Contracts.DbExport/"]
COPY ["Infrastructure/Cargowhale.Infrastructure.Core/Cargowhale.Infrastructure.Core.csproj", "Infrastructure/Cargowhale.Infrastructure.Core/"]
COPY ["Domain/Cargowhale.Domain.Processing/Cargowhale.Domain.Processing.csproj", "Domain/Cargowhale.Domain.Processing/"]
RUN dotnet restore "Application/Cargowhale/Cargowhale.csproj"
COPY . .
WORKDIR "/src/Application/Cargowhale"
RUN dotnet build "Cargowhale.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Cargowhale.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cargowhale.dll"]