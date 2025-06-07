using System.Globalization;
using ApplicationCore.Commons.Interfaces.Repositories;
using ApplicationCore.Commons.Interfaces.Services.VideoGame.Admin;
using ApplicationCore.Commons.Models;
using ApplicationCore.Commons.Models.Parts;
using ApplicationCore.Dto.Request.VideoGame;
using CsvHelper;

public class VideoGameCsvImportService : IVideoGameCsvImportService
{

    private readonly IGenericRepository<VideoGame, int> _videoGameRepo;
    private readonly IGenericRepository<GameImage, int> _imageRepo;
    private readonly IGenericRepository<GameTitle, int> _titleRepo;
    private readonly IGenericRepository<GameConsole, int> _consoleRepo;
    private readonly IGenericRepository<GameGenre, int> _genreRepo;
    private readonly IGenericRepository<GamePublisher, int> _publisherRepo;
    private readonly IGenericRepository<GameDeveloper, int> _developerRepo;

    public VideoGameCsvImportService(
        IGenericRepository<VideoGame, int> videoGameRepo,
        IGenericRepository<GameImage, int> imageRepo,
        IGenericRepository<GameTitle, int> titleRepo,
        IGenericRepository<GameConsole, int> consoleRepo,
        IGenericRepository<GameGenre, int> genreRepo,
        IGenericRepository<GameDeveloper, int> developerRepo,
        IGenericRepository<GamePublisher, int> publisherRepo
    )
    {
        _videoGameRepo = videoGameRepo;
        _imageRepo = imageRepo;
        _titleRepo = titleRepo;
        _consoleRepo = consoleRepo;
        _genreRepo = genreRepo;
        _developerRepo = developerRepo;
        _publisherRepo = publisherRepo;
    }

    private Dictionary<string, GameImage> _imageCache = new();
    private Dictionary<string, GameTitle> _titleCache = new();
    private Dictionary<string, GameConsole> _consoleCache = new();
    private Dictionary<string, GameGenre> _genreCache = new();
    private Dictionary<string, GamePublisher> _publisherCache = new();
    private Dictionary<string, GameDeveloper> _developerCache = new();


    public async Task<int> UploadCsvAsync(Stream fileStream)
    {

        _videoGameRepo.DisableChangeTracking();
        await ImportToCache();

        using var reader = new StreamReader(fileStream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<VideoGameCsvMapper>();

        var records = csv.GetRecords<VideoGameRequestDto>().ToList();

        var batchSize = 1000;

        var total = 0;

        for (int i = 0; i < records.Count; i += batchSize)
        {

            var batchRecords = records.Skip(i).Take(batchSize);

            var batchGames = new List<VideoGame>();

            foreach (var dto in batchRecords)
            {
                var image = await GetOrCreateImageAsync(dto.ImageLink);
                var title = await GetOrCreateTitleAsync(dto.Title);
                var console = await GetOrCreateConsoleAsync(dto.Console);
                var genre = await GetOrCreateGenreAsync(dto.Genre);
                var publisher = await GetOrCreatePublisherAsync(dto.Publisher);
                var developer = await GetOrCreateDeveloperAsync(dto.Developer);

                var videoGame = new VideoGame
                {
                    Image = image,
                    Title = title,
                    Console = console,
                    Genre = genre,
                    Publisher = publisher,
                    Developer = developer,
                    CriticScore = dto.CriticScore,
                    Sales = new GameSales
                    {
                        TotalSales = dto.TotalSales,
                        NaSales = dto.NaSales,
                        JpSales = dto.JpSales,
                        PalSales = dto.PalSales,
                        OtherSales = dto.OtherSales
                    },
                    ReleaseDate = dto.ReleaseDate,
                    LastUpdate = dto.LastUpdate
                };


                batchGames.Add(videoGame);
            }

            await _videoGameRepo.AddRangeAsync(batchGames);
            await _videoGameRepo.SaveChangesAsync();
            total += batchGames.Count;
        }

        _videoGameRepo.EnableChangeTracking();
        return total;

    }

    public async Task ImportToCache()
    {
        var images = await _imageRepo.FindAllAsync();
        _imageCache = images.ToDictionary(c => c.Link, StringComparer.OrdinalIgnoreCase);

        var titles = await _titleRepo.FindAllAsync();
        _titleCache = titles.ToDictionary(c => c.Name, StringComparer.OrdinalIgnoreCase);

        var consoles = await _consoleRepo.FindAllAsync();
        _consoleCache = consoles.ToDictionary(c => c.Name, StringComparer.OrdinalIgnoreCase);

        var genres = await _genreRepo.FindAllAsync();
        _genreCache = genres.ToDictionary(c => c.Name, StringComparer.OrdinalIgnoreCase);

        var publishers = await _publisherRepo.FindAllAsync();
        _publisherCache = publishers.ToDictionary(c => c.Name, StringComparer.OrdinalIgnoreCase);

        var developers = await _developerRepo.FindAllAsync();
        _developerCache = developers.ToDictionary(c => c.Name, StringComparer.OrdinalIgnoreCase);
    }

    private async Task<GameImage> GetOrCreateImageAsync(string link)
    {
        if (_imageCache.TryGetValue(link, out var existing))
        {
            return existing;
        }

        var newValue = new GameImage { Link = link };
        await _imageRepo.AddAsync(newValue);
        _imageCache[link] = newValue;
        return newValue;
    }


    private async Task<GameTitle> GetOrCreateTitleAsync(string name)
    {
        if (_titleCache.TryGetValue(name, out var existing))
        {
            return existing;
        }

        var newValue = new GameTitle { Name = name };
        await _titleRepo.AddAsync(newValue);
        _titleCache[name] = newValue;
        return newValue;
    }

    private async Task<GameConsole> GetOrCreateConsoleAsync(string name)
    {
        if (_consoleCache.TryGetValue(name, out var existing))
        {
            return existing;
        }

        var newValue = new GameConsole { Name = name };
        await _consoleRepo.AddAsync(newValue);
        _consoleCache[name] = newValue;
        return newValue;
    }

    private async Task<GameGenre> GetOrCreateGenreAsync(string name)
    {
        if (_genreCache.TryGetValue(name, out var existing))
        {
            return existing;
        }

        var newValue = new GameGenre { Name = name };
        await _genreRepo.AddAsync(newValue);
        _genreCache[name] = newValue;
        return newValue;
    }
    private async Task<GamePublisher> GetOrCreatePublisherAsync(string name)
    {
        if (_publisherCache.TryGetValue(name, out var existing))
        {
            return existing;
        }

        var newValue = new GamePublisher { Name = name };
        await _publisherRepo.AddAsync(newValue);
        _publisherCache[name] = newValue;
        return newValue;
    }

    private async Task<GameDeveloper> GetOrCreateDeveloperAsync(string name)
    {
        if (_developerCache.TryGetValue(name, out var existing))
        {
            return existing;
        }

        var newValue = new GameDeveloper { Name = name };
        await _developerRepo.AddAsync(newValue);
        _developerCache[name] = newValue;
        return newValue;
    }
}