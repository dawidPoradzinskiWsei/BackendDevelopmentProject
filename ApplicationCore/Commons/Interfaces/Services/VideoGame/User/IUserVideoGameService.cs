using ApplicationCore.Commons.Interfaces.Specifications;
using ApplicationCore.Commons.Models;
using ApplicationCore.Dto.Response;

public interface IUserVideoGameService
{
    Task<VideoGameResponseDto?> GetVideoGameDtoByIdAsync(int id);
    Task<PagedList<VideoGameResponseDto>> GetFilteredAsync(VideoGameParameters parameters);
    Task<VideoGame?> GetByIdAsync(int id);
    Task<UserScore> AddUserScoreAsync(int videoGameId, int userId, float score);
    Task DeleteUserScoreAsync(int videoGameId, int userId);
    Task<UserScore?> GetUserScoreAsync(int videoGameId, int userId);
    Task<UserScore> UpdateUserScoreAsync(int videoGameId, int userId, float score);
    Task<PagedList<UserScore>> GetAllUserScoresAsync(int videoGameId, PaginationParameters parameters);
}