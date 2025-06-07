using ApplicationCore.Commons.Interfaces.Repositories;
using ApplicationCore.Commons.Interfaces.Services.Publisher;
using ApplicationCore.Commons.Models.Parts;

public class PublisherService : IPublisherService
{

    private readonly IGenericRepository<GamePublisher, int> _repo;

    public PublisherService(IGenericRepository<GamePublisher, int> repo)
    {
        _repo = repo;
    }
    public async Task<bool> DeleteByIdAsync(int id)
    {
        var publisher = await GetByIdAsync(id);

        if (publisher is null)
        {
            return false;
        }

        await _repo.RemoveAsync(publisher);
        await _repo.SaveChangesAsync();
        return true;
    }

    public async Task<GamePublisher> GetByIdAsync(int id)
    {
        return await _repo.FindByIdAsync(id);
    }

    public async Task<bool> UpdateByIdAsync(int id, string name)
    {
        var publisher = await GetByIdAsync(id);

        if (publisher is null)
        {
            return false;
        }

        publisher.Name = name;
        await _repo.SaveChangesAsync();
        return true;
    }

    public async Task<PagedList<GamePublisher>> GetFilteredAsync(NameParameters parameters)
    {
        var spec = new FilterPublisherByNameSpec(parameters);

        var baseQuery = _repo.FindBySpecification(spec);

        return PagedList<GamePublisher>.ToPagedList(baseQuery, parameters.PageNumber, parameters.PageSize);
    }

    public async Task<GamePublisher> CreateAsync(string name)
    {
        var publisher = _repo.FindBySpecification(new FilterPublisherByNameSpec(new NameParameters { Name = name })).SingleOrDefault();

        if (publisher is not null)
        {
            return null;
        }

        var newPublisher = new GamePublisher { Name = name };
        await _repo.AddAsync(newPublisher);
        await _repo.SaveChangesAsync();

        return newPublisher;
    }
}