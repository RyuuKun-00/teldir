
FROM  mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 4001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .
COPY backend/*.csproj ./backend/
COPY backend.Core/*.csproj ./backend.Core/
COPY backend.Application/*.csproj ./backend.Application/
COPY backend.DataAccess/*.csproj ./backend.DataAccess/
COPY backend.Migrations/*.csproj ./backend.Migrations/
RUN dotnet restore
COPY . .
WORKDIR "/src/backend"
RUN dotnet build "backend.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/sdk:8.0  AS migration
WORKDIR /src
COPY . .
RUN dotnet restore "backend.Migrations/backend.Migrations.csproj"
COPY . .
WORKDIR "/src/backend.Migrations"
RUN dotnet build "backend.Migrations.csproj" -c Release -o /app/migration

FROM build AS publish
RUN dotnet publish "backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /migration
COPY --from=migration /app/migration .

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "backend.dll"]