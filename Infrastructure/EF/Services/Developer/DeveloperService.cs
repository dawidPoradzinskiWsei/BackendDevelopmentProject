using ApplicationCore.Commons.Interfaces.Repositories;
using ApplicationCore.Commons.Interfaces.Services.Developer;
using ApplicationCore.Commons.Models.Parts;
using Microsoft.EntityFrameworkCore;

public class DeveloperService : IDeveloperPublisherService<GameDeveloper>
{
    private readonly IGenericRepository<GameDeveloper, int> _repo;

    public DeveloperService(IGenericRepository<GameDeveloper, int> repo)
    {
        _repo = repo;
    }

    public async Task<GameDeveloper> CreateAsync(string name)
    {
        var developer = _repo.FindBySpecification(new FilterDeveloperByNameSpec(new NameParameters { Name = name })).SingleOrDefault();

        if (developer is not null)
        {
            throw new EntityAlreadyExistException();
        }

        var newDeveloper = new GameDeveloper { Name = name };
        await _repo.AddAsync(newDeveloper);
        await _repo.SaveChangesAsync();

        return newDeveloper;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var developer = await GetByIdAsync(id);

        if (developer is null)
        {
            throw new KeyNotFoundException();
        }

        await _repo.RemoveAsync(developer);
        await _repo.SaveChangesAsync();
        return true;
    }

    public async Task<GameDeveloper> GetByIdAsync(int id)
    {
        var result = await _repo.FindByIdAsync(id);

        if (result is null)
        {
            throw new KeyNotFoundException();
        }

        return result;
    }

    public async Task<PagedList<GameDeveloper>> GetFilteredAsync(NameParameters parameters)
    {
        var spec = new FilterDeveloperByNameSpec(parameters);

        var baseQuery = _repo.FindBySpecification(spec);

        return PagedList<GameDeveloper>.ToPagedList(baseQuery, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<bool> UpdateByIdAsync(int id, string name)
    {
        var developer = await GetByIdAsync(id);

        if (developer is null)
        {
            throw new KeyNotFoundException();
        }

        developer.Name = name;
        await _repo.SaveChangesAsync();
        return true;
    }
}