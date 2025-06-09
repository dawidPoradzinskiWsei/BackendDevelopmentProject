using ApplicationCore.Commons.Models;
using ApplicationCore.Dto.Request.VideoGame;
using ApplicationCore.Dto.Response;


public interface IAdminVideoGameService
{
    Task<VideoGameResponseDto> AddVideoGameAsync(VideoGameRequestDto game);
    Task<VideoGameResponseDto> UpdateByIdAsync(int id, VideoGameRequestDto game);
    Task<bool> DeleteByIdAsync(int id);
}