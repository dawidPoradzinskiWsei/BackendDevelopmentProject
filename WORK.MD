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

  1. Stworzyłem ogólnego sln
  2. Stworzyłem projekt Infrasturucture i dodałem do niego połączenie core
  3. Zaimplementowanie IGenericRepository oraz IVideoGameRepository na klasy
  4. Zmapowałem `VideoGame` na bazę danych, ale nie wszystko działa (Patrz `TODO 1.`)
  5. Nie korzystałem z GameStatistics, bo nie mogłem tego znaleźć na podglądzie pobieranych danych. Jeśli potrzebne, to daj znać i dodam
  6. Aplikacja na razie nie ma użytkowników (Patrz `TODO 2.`)
  7. Dodalem połączenie z sqlite, aby móc na razie łatwo destować bez osobnych baz
  8. Stworzyłem aplikacje WebApi, połączyłem z Infrastructure i jedynie dodałem do niej DbContext z VideoGame
  

  # TODO

  1. Potrzebuję przerzucenia Title,Platform,CoverImageUrl do osobnych klas, ponieważ EF wywala błąd, że nie mogę stworzyć osobnej tabeli tylko z stringa
  2. Potrzebuję stworzenie nowego modelu UserDetails z interface IIdentity<string> oraz defaultowym konstruktorem, aby móc dodać użytkowników do aplikacji
  3. Zrobienia w ApplicationCore w folderze Interfaces dwóch folderów. AdminService oraz userService. Każdy z nich ma mieć swój interface i klasę
  4. W interface tworzysz jakie rzeczy mają dziać się w danym serwisie. np:
  ```
  IEnumerable<VideoGame> findAllVideoGames();
  VideoGame? findVideoGameById(int id);
  VideoGame? findVideoGameByTitle(string title);
  ```
  5. Następnie w klasie implementujesz dany interface
  *WAŻNE*
  W danej klasie musisz dodać wstrzyknięcie potrzebych repositoriów, które wcześniej stworzyłeś, czyli np:
  ```
  private readonly IVideoGameRepostiory<VideoGame, int> _videoRepository;
  
  public UserService(IGenericRepository<VideoGame, int> videoRepository )
  {
    _videoRepository = videoRepostiory,
  }
  ```

 Oczywiście tutaj nie pracujesz na prawdziwych danych, ale pokazujesz co chcesz osiągnąć, a ja potem zrobię tego implementację, aby serio robiło rzeczy w bazie danych

 ```
 public VideoGame FindVideoGameById(int id)
 {
  return _videoRepository.FindById() // <-- nazwy brane z I...Repository w folderze Commons / Repository
 }
 ```
 6. Możesz zacząć bawić się WebAPI. Dodać do niego kontrolery, co ma być zwracane pod jakimi linkami itd. Fajnie, gdybyś w readme.md wypisywał, co jaki link przyjmuje i co zwraca
 7. Możesz ogarnąć do webAPI co było dawane na insturkcjach z labów 5 oraz 6, bo ktoś musi zrobić tokeny JWT
  

# Infrastructure

# WebAPI

# Tests