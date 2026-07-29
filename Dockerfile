# --- Этап 1: Сборка (SDK) ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# 1. Копируем файл проекта и восстанавливаем NuGet-пакеты (кэшируется Докером)
COPY ["Notes.Manager.csproj", "./"]
RUN dotnet restore "Notes.Manager.csproj"

# 2. Копируем остальные исходники и собираем релиз
COPY . ./
RUN dotnet publish "Notes.Manager.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- Этап 2: Запуск (Runtime) ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Копируем только скомпилированное приложение из этапа build
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "Notes.Manager.dll"]