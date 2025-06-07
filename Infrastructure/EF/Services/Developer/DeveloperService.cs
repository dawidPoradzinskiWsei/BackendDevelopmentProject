using ApplicationCore.Commons.Interfaces.Repositories;
using ApplicationCore.Commons.Interfaces.Services.Developer;
using ApplicationCore.Commons.Models.Parts;

public class DeveloperPublisherService : IDeveloperPublisherService<GameDeveloper>
{
    private readonly IGenericRepository<GameDeveloper, int> _repo;

    public DeveloperPublisherService(IGenericRepository<GameDeveloper, int> repo)
    {
        _repo = repo;
    }

    public async Task<GameDeveloper> CreateAsync(string name)
    {
        var developer = _repo.FindBySpecification(new FilterDeveloperByNameSpec(new NameParameters { Name = name })).SingleOrDefault();

        if (developer is not null)
        {
            return null;
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
            return false;
        }

        await _repo.RemoveAsync(developer);
        await _repo.SaveChangesAsync();
        return true;
    }

    public async Task<GameDeveloper> GetByIdAsync(int id)
    {
        return await _repo.FindByIdAsync(id);
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
            return false;
        }

        developer.Name = name;
        await _repo.SaveChangesAsync();
        return true;
    }
}