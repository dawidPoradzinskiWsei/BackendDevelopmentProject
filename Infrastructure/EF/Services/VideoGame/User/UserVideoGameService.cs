using ApplicationCore.Commons.Interfaces.Repositories;
using ApplicationCore.Commons.Models;
using ApplicationCore.Dto.Response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class UserVideoGameService : IUserVideoGameService
{
    private readonly IGenericRepository<VideoGame, int> _repo;
    private readonly IUserScoreRepository _userRepo;
    private readonly IMapper _mapper;
    public UserVideoGameService(IGenericRepository<VideoGame, int> repo, IUserScoreRepository userRepo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
        _userRepo = userRepo;
    }

    public async Task<VideoGameResponseDto?> GetVideoGameDtoByIdAsync(int id)
    {
        var spec = new FilterVideoGameByIdSpec(id);

        var baseQuery = _repo.FindBySpecification(spec).Select(x => _mapper.Map<VideoGameResponseDto>(x)).FirstOrDefault();

        return baseQuery;
    }

    public async Task<PagedList<VideoGameResponseDto>> GetFilteredAsync(VideoGameParameters parameters)
    {
        var spec = new FilterVideoGameFullSpec(parameters);

        var baseQuery = _repo.FindBySpecification(spec).Select(x => _mapper.Map<VideoGameResponseDto>(x));

        return PagedList<VideoGameResponseDto>.ToPagedList(baseQuery, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<VideoGame?> GetByIdAsync(int id)
    {
        return await _repo.FindByIdAsync(id);
    }

    public async Task<UserScore> AddUserScoreAsync(int videoGameId, int userId, float score)
    {
        var videoGame = await _repo.FindByIdAsync(videoGameId);

        if (videoGame is null)
        {
            throw new KeyNotFoundException();
        }

        var existingScore = await _userRepo.GetUserScoreAsync(videoGameId, userId);

        if (existingScore is not null)
        {
            throw new InvalidOperationException();
        }

        var userScore = new UserScore
        {
            VideoGameId = videoGameId,
            UserId = userId,
            Score = score
        };

        await _userRepo.AddAsync(userScore);

        return userScore;

    }

    public async Task DeleteUserScoreAsync(int videoGameId, int userId)
    {
        var userScore = await _userRepo.GetUserScoreAsync(videoGameId, userId);

        if (userScore is null)
        {
            throw new KeyNotFoundException();
        }

        await _userRepo.RemoveAsync(userScore);
    }

    public async Task<UserScore?> GetUserScoreAsync(int videoGameId, int userId)
    {
        return await _userRepo.GetUserScoreAsync(videoGameId, userId);
    }

    public async Task<UserScore> UpdateUserScoreAsync(int videoGameId, int userId, float score)
    {
        var userScore = await _userRepo.GetUserScoreAsync(videoGameId, userId);

        if (userScore is null)
        {
            throw new KeyNotFoundException();
        }

        userScore.Score = score;
        await _userRepo.UpdateAsync(userScore);
        return userScore;
    }

    public async Task<PagedList<UserScore>> GetAllUserScoresAsync(int videoGameId, PaginationParameters parameters)
    {
        var result = await _userRepo.GetAllUserScoresAsync(videoGameId);

        return PagedList<UserScore>.ToPagedList(result, parameters.PageNumber, parameters.PageSize);
    }
}