using System.Security.Principal;
using ApplicationCore.Commons.Interfaces;
using ApplicationCore.Commons.Interfaces.Repositories;
using ApplicationCore.Commons.Interfaces.Services.Developer;

public class GenericNameService<T> : IGenericNameService<T>
    where T : NameEntity, new()
{

    private readonly IGenericRepository<T, int> _repository;

    public GenericNameService(IGenericRepository<T, int> repository)
    {
        _repository = repository;
    }

    public async Task<T> CreateAsync(string name)
    {
        var spec = new NameSpecification<T>(new NameParameters { Name = name });

        var result = _repository.FindBySpecification(spec);

        if (result.Count() != 0)
        {
            throw new InvalidOperationException();
        }

        var entity = new T();

        entity.Name = name;

        await _repository.AddAsync(entity);
        await _repository.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var entity = await _repository.FindByIdAsync(id);
        if (entity is null)
        {
            throw new KeyNotFoundException();
        }

        await _repository.RemoveAsync(entity);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<PagedList<T>> GetFilteredAsync(NameParameters parameters)
    {
        var spec = new NameSpecification<T>(parameters);
        var baseQuery = _repository.FindBySpecification(spec);

        return PagedList<T>.ToPagedList(baseQuery.AsQueryable(), parameters.PageNumber, parameters.PageSize);
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _repository.FindByIdAsync(id);
    }

    public async Task<T> UpdateByIdAsync(int id, string name)
    {
        var entity = await _repository.FindByIdAsync(id);
        if (entity is null)
        {
            throw new KeyNotFoundException();
        }

        var nameProperty = typeof(T).GetProperty("Name");
        if (nameProperty is null)
        {
            throw new InvalidOperationException("The generic type must have a 'Name' property.");
        }

        nameProperty.SetValue(entity, name);
        await _repository.UpdateAsync(entity);
        await _repository.SaveChangesAsync();
        return entity;
    }
}