# ApplicationCore

### 16.04.2025 DP

    # Zrobione
    INFO.MD z podstawowymi rzeczami do zrobienia

    # TODO
    1. Potrzebuję modele, aby móc utworzyć bazę danych
    2. Potrzebuję interface repozytoriów, aby móc zaczać tworzyć ich implementacje

### KR
    # Robisz lepsze instrukcje od Siwonia xD
    # Te repository itd co pisałes poprostu ukradlem od niego nic nie zmienialem
    # Zrobiłem to co było w instrukcji mam nadzieję ze git, modeli troche wyszlo ale stwierdzilem ze zrobie a jak sie nie przyda
      to nie trzeba bedzie uzywac
    # pisz mes jakbyś już dodał mi jakąś robote bo na gita nie wchodze tak czesto a powiadomienie na mesie zauwaze szybciej.
    
    # I nwm jakos zle mi sie klonuje z tego gita chyba jakby nie robi mi sln jak dodaje projekt probowalem ta komende co mi dales 

### 26.04.2025 DP

  # Info zwrotne

  1. Stworzyłem ogólnego sln oraz dodałem połączenie core -> infr -> webapi
  1. Nie korzysdtałem z GameStatistics, bo nie mogłem tego znaleźć na podglądzie pobieranych danych. Jeśli potrzebne, to daj znać i dodam

  # TODO

  1. Potrzebuję stworzenie nowego modelu UserDetails z interface IIdentity<string> oraz defaultowym konstruktorem, aby móc dodać użytkowników do aplikacji
  2. Potrzebuję przerzucenia Title,Platform,CoverImageUrl do osobnych klas, ponieważ EF wywala błąd, że nie mogę stworzyć osobnej tabeli tylko z stringa
  

# Infrastructure

### 26.04.2025 DP

  # Zrobione

  1. Stworzenie modelu do bazy danych na podstawie VideoGame:  Genre, Publisher, Developer jako osobne tabele. Reszta jako pola w VideoGame

# WebAPI

  # Zrobione

  1. Stworzenie projektu WebAPI
  2. Dodanie do niego DBContext, aby móc zrobić migracje na infrastructure
  3. Dodanie folderu sqlite oraz zmian w appsettings, aby baza była na razie na sqlite

# Tests