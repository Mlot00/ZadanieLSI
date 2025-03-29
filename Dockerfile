# Stage 1: Build Angular app
FROM node:18.19 AS node-builder
WORKDIR /app

# Kopiowanie plików package.json i package-lock.json 
COPY webApp/package*.json ./
RUN npm install

# Kopiowanie reszty kodu front-end
COPY webApp/ .
# build produkcyjny Angulara
RUN npm run build

# Stage 2: Build .NET API
FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src

# Kopiowanie pliku .csproj i przywracanie zależności
COPY WebAPI/WebAPI/WebAPI.csproj WebAPI/WebAPI/
RUN dotnet restore "WebAPI/WebAPI/WebAPI.csproj"

# Kopiowanie reszty kodu .NET
COPY WebAPI/WebAPI/ WebAPI/WebAPI/

WORKDIR "/src/WebAPI/WebAPI"

# build project
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

# Stage 3: Publikacja
FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish

# Stage 4: Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS final
WORKDIR /app

# Kopiowanie opublikowanego backendu
COPY --from=publish /app/publish .
# Kopiowanie zbudowanych plików Angulara do katalogu wwwroot
COPY --from=node-builder /app/dist/web-app/browser ./wwwroot

EXPOSE 80
ENTRYPOINT ["dotnet", "WebAPI.dll"]