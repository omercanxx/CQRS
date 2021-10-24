#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CQRS.API/CQRS.API.csproj", "CQRS.API/"]
COPY ["CQRS.Domain/CQRS.Domain.csproj", "CQRS.Domain/"]
COPY ["CQRS.Infrastructure/CQRS.Infrastructure.csproj", "CQRS.Infrastructure/"]
COPY ["CQRS.Core/CQRS.Core.csproj", "CQRS.Core/"]
COPY ["CQRS.Application/CQRS.Application.csproj", "CQRS.Application/"]
RUN dotnet restore "CQRS.API/CQRS.API.csproj"
COPY . .
WORKDIR "/src/CQRS.API"
RUN dotnet build "CQRS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CQRS.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CQRS.API.dll"]
