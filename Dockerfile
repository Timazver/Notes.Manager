FROM mcr.microsoft.com/dotnet/sdk:10.0 AS base
WORKDIR /src

COPY ["Notes.Manager.csproj", "./"]
RUN dotnet restore "Notes.Manager.csproj"
COPY . .
RUN dotnet tool restore

FROM base AS build-ef-bundle
ENV DB_HOST=localhost \
    DB_PORT=5432 \
    DB_NAME=design_time \
    DB_USER=design_time \
    DB_PASS=design_time
RUN dotnet ef migrations bundle --self-contained --target-runtime linux-amd64 --output /app/ef_bundle

FROM base AS build-notes-api
RUN dotnet publish "Notes.Manager.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore

FROM mcr.microsoft.com/dotnet/runtime-deps:10.0 as bundle-exec
WORKDIR /app
COPY --from=build-ef-bundle --chmod=755 /app/ef_bundle ./ef_bundle

RUN <<'EOF' cat > /app/migrate.sh
#!/bin/sh
set -eu

: "${DB_HOST:?DB_HOST is required}"
: "${DB_PORT:?DB_PORT is required}"
: "${DB_NAME:?DB_NAME is required}"
: "${DB_USER:?DB_USER is required}"
: "${DB_PASS:?DB_PASS is required}"

CONNECTION_STRING="Host=${DB_HOST};Port=${DB_PORT};Database=${DB_NAME};Username=${DB_USER};Password=${DB_PASS}"

exec ./ef_bundle --connection "$CONNECTION_STRING"
EOF
RUN chmod +x /app/migrate.sh
ENTRYPOINT ["./migrate.sh"]

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS api-exec
WORKDIR /app
COPY --from=build-notes-api /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Notes.Manager.dll"]