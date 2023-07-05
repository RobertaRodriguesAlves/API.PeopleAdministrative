#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0.16-jammy-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0.408-jammy-amd64 AS build
WORKDIR /src
COPY ["/src/API.PeopleAdministrative.PublicApi/API.PeopleAdministrative.PublicApi.csproj", "API.PeopleAdministrative.PublicApi/"] 
COPY ["/src/API.PeopleAdministrative.Application/API.PeopleAdministrative.Application.csproj", "API.PeopleAdministrative.Application/"]
COPY ["/src/API.PeopleAdministrative.Domain/API.PeopleAdministrative.Domain.csproj", "API.PeopleAdministrative.Domain/"]
COPY ["/src/API.PeopleAdministrative.Shared/API.PeopleAdministrative.Shared.csproj", "API.PeopleAdministrative.Shared/"]
COPY ["/src/API.PeopleAdministrative.Infrastructure/API.PeopleAdministrative.Infrastructure.csproj", "API.PeopleAdministrative.Infrastructure/"]
RUN dotnet restore "/src/API.PeopleAdministrative.PublicApi/API.PeopleAdministrative.PublicApi.csproj"
WORKDIR "/src/API.PeopleAdministrative.PublicApi/"
COPY . .
RUN dotnet build "API.PeopleAdministrative.PublicApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.PeopleAdministrative.PublicApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.PeopleAdministrative.PublicApi.dll"]