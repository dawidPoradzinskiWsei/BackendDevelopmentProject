using ApplicationCore.Commons.Models.Parts;

namespace ApplicationCore.Commons.Interfaces.Services.Developer;

public interface IGenericNameService<T>
{
    Task<T> GetByIdAsync(int id);
    Task<T> UpdateByIdAsync(int id, string name);
    Task<bool> DeleteByIdAsync(int id);
    Task<T> CreateAsync(string name);
    Task<PagedList<T>> GetFilteredAsync(NameParameters parameters);
}