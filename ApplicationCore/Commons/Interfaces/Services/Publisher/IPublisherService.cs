using ApplicationCore.Commons.Models.Parts;

namespace ApplicationCore.Commons.Interfaces.Services.Publisher;

public interface IPublisherService
{
    Task<GamePublisher> GetByIdAsync(int id);
    Task<bool> UpdateByIdAsync(int id, string name);
    Task<bool> DeleteByIdAsync(int id);
    Task<GamePublisher> CreateAsync(string name);
    Task<PagedList<GamePublisher>> GetFilteredAsync(NameParameters parameters);
}