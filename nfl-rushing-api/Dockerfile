#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /app

COPY ./nfl-rushing-api ./
RUN dotnet restore nfl-rushing-api.csproj

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "nfl-rushing-api.dll"]