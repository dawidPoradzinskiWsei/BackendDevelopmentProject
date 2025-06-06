using ApplicationCore.Commons.Models.Parts;

namespace ApplicationCore.Commons.Interfaces.Services.Developer;

public interface IDeveloperService
{
    Task<GameDeveloper> GetByIdAsync(int id);
    Task<bool> UpdateByIdAsync(int id, GameDeveloper publisher);
    Task<bool> DeleteByIdAsync(int id);
}