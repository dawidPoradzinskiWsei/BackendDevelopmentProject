using ApplicationCore.Commons.Models;
using ApplicationCore.Dto.Response;


public interface IAdminVideoGameService
{
    Task<VideoGameResponseDto> AddVideoGameAsync(VideoGame game);
    Task<bool> UpdateByIdAsync(int id, VideoGame game);
    Task<bool> DeleteByIdAsync(int id);
}