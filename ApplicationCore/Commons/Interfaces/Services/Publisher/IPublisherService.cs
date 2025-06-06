using ApplicationCore.Commons.Models.Parts;

namespace ApplicationCore.Commons.Interfaces.Services.Publisher;

public interface IPublisherService
{
    Task<GamePublisher> GetByIdAsync(int id);
    Task<bool> UpdateByIdAsync(int id, GamePublisher publisher);
    Task<bool> DeleteByIdAsync(int id);
}