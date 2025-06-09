using System.Threading.Tasks;
using ApplicationCore.Commons.Interfaces.Repositories;
using ApplicationCore.Commons.Models;
using ApplicationCore.Commons.Models.Parts;
using ApplicationCore.Dto.Request.VideoGame;
using ApplicationCore.Dto.Response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

public class AdminVideoGameService : IAdminVideoGameService
{
    private readonly IGenericRepository<VideoGame, int> _repo;
    private readonly IGenericRepository<GameImage, int> _imageRepo;
    private readonly IGenericRepository<GameTitle, int> _titleRepo;
    private readonly IGenericRepository<GameConsole, int> _consoleRepo;
    private readonly IGenericRepository<GameGenre, int> _genreRepo;
    private readonly IGenericRepository<GameDeveloper, int> _developerRepo;
    private readonly IGenericRepository<GamePublisher, int> _publisherRepo;
    private readonly IMapper _mapper;

    public AdminVideoGameService(
        IGenericRepository<VideoGame, int> repo,
        IGenericRepository<GameImage, int> imageRepo,
        IGenericRepository<GameTitle, int> titleRepo,
        IGenericRepository<GameConsole, int> consoleRepo,
        IGenericRepository<GameGenre, int> genreRepo,
        IGenericRepository<GameDeveloper, int> developerRepo,
        IGenericRepository<GamePublisher, int> publisherRepo,
        IMapper mapper
    )
    {
        _repo = repo;
        _imageRepo = imageRepo;
        _titleRepo = titleRepo;
        _consoleRepo = consoleRepo;
        _genreRepo = genreRepo;
        _developerRepo = developerRepo;
        _publisherRepo = publisherRepo;
        _mapper = mapper;
    }

    public async Task<VideoGameResponseDto> AddVideoGameAsync([FromBody] VideoGameRequestDto game)
    {
        VideoGame newVideoGame = new VideoGame();
        
        await MapRequestToEntityWithNavTables(newVideoGame, game);

        var result = await _repo.AddAsync(newVideoGame);
        await _repo.SaveChangesAsync();

        return _mapper.Map<VideoGameResponseDto>(result);

    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var result = await _repo.FindByIdAsync(id);
        if (result is null)
        {
            throw new KeyNotFoundException();
        }

        await _repo.RemoveAsync(result);
        await _repo.SaveChangesAsync();
        return true;
    }

    public async Task<VideoGameResponseDto> UpdateByIdAsync(int id, [FromBody] VideoGameRequestDto game)
    {
        var result = await _repo.FindByIdAsync(id);
        if (result is null)
        {
            throw new KeyNotFoundException();
        }
        await MapRequestToEntityWithNavTables(result, game);
        await _repo.UpdateAsync(result);
        await _repo.SaveChangesAsync();
        return _mapper.Map<VideoGameResponseDto>(result);

    }

    private async Task<VideoGame> MapRequestToEntityWithNavTables(VideoGame newVideoGame, VideoGameRequestDto game)
    {

        if (!string.IsNullOrWhiteSpace(game.ImageLink))
        {
            newVideoGame.Image = await _imageRepo.GetOrCreateAsync(
                x => x.Link == game.ImageLink,
                new GameImage { Link = game.ImageLink });
        }
        if (!string.IsNullOrWhiteSpace(game.Title))
        {
            newVideoGame.Title = await _titleRepo.GetOrCreateAsync(
                x => x.Name == game.Title,
                 new GameTitle { Name = game.Title });
        }
        if (!string.IsNullOrWhiteSpace(game.Console))
        {
            newVideoGame.Console = await _consoleRepo.GetOrCreateAsync(
                x => x.Name == game.Console,
                new GameConsole { Name = game.Console });
        }
        if (!string.IsNullOrWhiteSpace(game.Genre))
        {
            newVideoGame.Genre = await _genreRepo.GetOrCreateAsync(
                x => x.Name == game.Genre,
                new GameGenre { Name = game.Genre });
        }
        if (!string.IsNullOrWhiteSpace(game.Developer))
        {
            newVideoGame.Developer = await _developerRepo.GetOrCreateAsync(
                x => x.Name == game.Developer,
                new GameDeveloper { Name = game.Developer });
        }
        if (!string.IsNullOrWhiteSpace(game.Publisher))
        {
            newVideoGame.Publisher = await _publisherRepo.GetOrCreateAsync(
                x => x.Name == game.Publisher,
                new GamePublisher { Name = game.Publisher });
        }

        newVideoGame.CriticScore = game.CriticScore;
        newVideoGame.ReleaseDate = game.ReleaseDate;
        newVideoGame.LastUpdate = game.LastUpdate;

        newVideoGame.Sales = new GameSales
        {
            JpSales = game.JpSales,
            NaSales = game.NaSales,
            OtherSales = game.OtherSales,
            PalSales = game.PalSales,
            TotalSales = game.TotalSales
        };

        return newVideoGame;
    }
}