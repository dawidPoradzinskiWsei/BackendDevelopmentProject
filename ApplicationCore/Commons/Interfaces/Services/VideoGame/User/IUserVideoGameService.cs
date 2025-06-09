using ApplicationCore.Commons.Interfaces.Specifications;
using ApplicationCore.Commons.Models;
using ApplicationCore.Dto.Response;

public interface IUserVideoGameService
{
    Task<VideoGameResponseDto?> GetVideoGameDtoByIdAsync(int id);
    Task<PagedList<VideoGameResponseDto>> GetFilteredAsync(VideoGameParameters parameters);
    Task<VideoGame?> GetByIdAsync(int id);
}