# Zadanie dla LSI Software

## Technologie
- **.NET 8**
- **Angular 19**
- **MS SQL**
- **Docker**

Aplikacja integruje backend (.NET API) i frontend (Angular) przy użyciu multi-stage Dockerfile.
W przypadku Docker Compose connection string do bazy jest nadpisywany przez zmienną środowiskową: ConnectionStrings__WebAPIConnectionString


## Uruchomienie aplikacji w kontenerze Docker

1. Upewnij się, że masz zainstalowany i uruchomiony Docker Desktop.
2. Przejdź do folderu, w którym znajduje się plik `docker-compose.yml`.
3. W wierszu poleceń (Command Prompt) wpisz:
   ```bash
	docker compose up --build
4. Otwórz przeglądarkę i wpisz http://localhost – aplikacja powinna się wyświetlić.


## Uruchomienie aplikacji lokalnie (development)

1. Dostosuj connection string w WebAPI w pliku appsettings.Development.json do swojej lokalnej bazy danych.
2. Uruchom WebAPI (np. poprzez Visual Studio 2022) korzystając z HTTPS.
3. Przejdź do folderu webApp, w którym znajduje się aplikacja Angular.
4. W wierszu poleceń (Command Prompt) wpisz:
   ```bash
	ng serve
5. Aplikacja Angular będzie dostępna domyślnie pod adresem http://localhost:4200.