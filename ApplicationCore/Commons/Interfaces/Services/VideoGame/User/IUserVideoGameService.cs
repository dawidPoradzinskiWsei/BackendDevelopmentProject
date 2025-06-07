using ApplicationCore.Commons.Interfaces.Specifications;
using ApplicationCore.Dto.Response;

namespace ApplicationCore.Commons.Interfaces.Services.VideoGame.User;

public interface IUserVideoGameService
{
    Task<VideoGameResponseDto?> GetByIdAsync(int id);
    Task<PagedList<VideoGameResponseDto>> GetFilteredAsync(VideoGameParameters parameters);
}