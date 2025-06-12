## Przygotowanie środowiska

1. **W katalogu głównym projektu uruchom**:
```bash
dotnet restore
```

2. **Skonfiguruj bazę danych**

- W pliku appsettings.json ustaw `DatabaseType` na bazę, z której chcesz skorzystać.
(dostępne: sqlite, sqlserver)

- W pliku appsettings.json ustaw odpowiedni `ConnectionStrings` do połaczenia z bazą danych.

## Uruchomienie aplikacji

**Uruchom aplikację**

1. W katalogu projektu `WebAPI` uruchom:
```bash
dotnet run
```

ALBO

2. W katalogu projektu `WebAPI` stwórz wersję realease komendą:
```bash
dotnet publish -c Release -o ../publish
```

W folderze publish przeprowadź jeszcze raz konfiguracje bazy danych i:

1. Uruchom plik WebAPI.exe

ALBO

2. Uruchom komendę:
```bash
dotnet WebAPI.dll
```

## Korzystanie z API.

1. Większość endpointów wymaga autoryzacji i rangi ADMIN. Zalecamy najpierw stworzyć nowe konte poprzez enpoint `v1/Auth/register` i dodanie tokena Bearer.

## Import danych

Wyślij plik z danymi poprzez endpoint API (dostępny na swaggerze) `v1/videogame/upload`.