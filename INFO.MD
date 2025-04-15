# Projekt

1. stwórz nowy plik sln, który będzie zawierał wszystkie projekty:

```bash
dotnet new sln --name KRDP_BackendProject
```

2. Po skończonej pracy wysyłaj wszystko (najlepiej najpierw dla każdego projektu zrób dotnet clean aby nie wysyłać śmieci) na githuba na swój branch kr_testing ORAZ w pliku WORK.MD pisz z czym sa problemy, co zrobiłeś albo co potrzebujesz do osiągnięcia celu

# ApplicationCore:

```
W tym projekcie tworzysz "pomysł" aplikacji. Jak mają wyglądać modele, jakie mamy mieć "sposoby" (bez konkretów) działania na tych danych w repozytoriach
```

### Tworzenie projektu
1. Stwórz nowy projekt typu `library`
```bash
dotnet new classlib -o ApplicationCore
```

### Rzeczy do zrobienia

```
Podstawowy kod klas możesz znaleźć na githubie Siwonia, który udostepnił na jednym z pierwszych zajęć na e.wsei
```

1. W folderze Commons\Repository stwórz interface IIdentity 

    ```
    Wykorzystamy go, do narzuczenia potrzeby posiadania ID 
    ```
    - Musi zawierać wymagane publiczne pole Id typu K z getterem i setterem
    - Musi zawierać funkcję CompareTo

2. W folderze Models stwórz główną klasę VideoGame z IIdentity<int>

    ```
    Będzie to nasz główny obiekt
    ```

    - Dodaj do niego wszystkie pola, które są dostępne z strony z danymi
    - Dla pól, które moga mieć takie same wartości dla rożnych gier zrób:

        a) Stwórz osobną klasę Game'Pole" z IIdentity<int>. 
        
        b) Przenieś pole z VideoGame do klasy

        c) Dodaj do VideoGame pole z klasą Game'Pole'

        #### GameDeveloper
        ```C#
        public class GameDeveloper : IIdentity<int>
        {
            public int Id {get;set;}
            public string Name {get;set;}
        }
        ```
        #### VideoGame
        ```C#
        public class VideoGame : IIdentity<int>
        {
            public int Id {get;set;}
            [...]
            public GameDeveloper Developer {get;set;}
            [...]
        }
        ```
3. W folderze Commons\Specification stwórz interface ISpecification
    ```
    Pozwoli on na wyszukiwanie i sortowanie zwracanych danych wedle kryteriów
    ```

4. Stwórz w folderze Commons\Repository interface IGenericRepository
    ```
    W nim tworzysz PODSTAWOWE metody do komunikacji z bazą danych
    ```

5. Dodatkowo w tym folderze stwórz interface`y bardziej zaawansowane np: IVideoGameRepository : IGenericRepository<VideoGame, int>

    ```
    W nich stwórz bardziej zaawansowane sposoby komunikacji z danymi np:
    Task<IEnumerable<VideoGame>> GetByGenreAsync(string genreName);
    ```
