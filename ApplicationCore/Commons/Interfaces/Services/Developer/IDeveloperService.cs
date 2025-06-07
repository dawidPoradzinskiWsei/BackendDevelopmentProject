using ApplicationCore.Commons.Models.Parts;

namespace ApplicationCore.Commons.Interfaces.Services.Developer;

public interface IDeveloperService
{
    Task<GameDeveloper> GetByIdAsync(int id);
    Task<bool> UpdateByIdAsync(int id, string name);
    Task<bool> DeleteByIdAsync(int id);
}