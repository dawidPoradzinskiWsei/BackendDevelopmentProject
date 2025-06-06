using ApplicationCore.Commons.Interfaces.Specifications;
using ApplicationCore.Dto.Response;

namespace ApplicationCore.Commons.Interfaces.Services.VideoGame.User;

public interface IUserVideoGameService
{
    Task<VideoGameResponseDto?> GetByIdAsync(int id);
    Task<IPagedList<VideoGameResponseDto>> GetFilteredAsync(IPaginationParameters parameters);
}