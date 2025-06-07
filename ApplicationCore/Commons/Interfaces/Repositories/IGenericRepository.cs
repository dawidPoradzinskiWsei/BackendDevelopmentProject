using System.Linq.Expressions;
using ApplicationCore.Commons.Interfaces.Specifications;

namespace ApplicationCore.Commons.Interfaces.Repositories;

public interface IGenericRepository<T, K> where T : IIdentity<K> where K : IComparable<K>
{
    void DisableChangeTracking();
    void EnableChangeTracking();
    Task<T?> FindByIdAsync(K id);
    Task<List<T>> FindAllAsync();
    Task AddRangeAsync(IEnumerable<T> o);
    Task<T> AddAsync(T o);
    Task RemoveAsync(T entity);
    Task UpdateAsync(T entity);
    IQueryable<T> FindBySpecification(ISpecification<T> specification = null);
    Task<int> SaveChangesAsync();
    Task<T> GetOrCreateAsync(Expression<Func<T, bool>> predicate, T newEntity);
}