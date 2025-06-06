using ApplicationCore.Dto.Response;

namespace ApplicationCore.Commons.Interfaces.Services.VideoGame.Admin;

public interface IAdminVideoGameService
{
    Task<VideoGameResponseDto> AddVideoGameAsync(Models.VideoGame game);
    Task<bool> UpdateByIdAsync(int id, Models.VideoGame game);
    Task<bool> DeleteByIdAsync(int id);
}