using System.Linq.Expressions;
using ApplicationCore.Commons.Interfaces;
using ApplicationCore.Commons.Interfaces.Repositories;
using ApplicationCore.Commons.Interfaces.Specifications;
using Microsoft.EntityFrameworkCore;

public class EFGenericRepository<T, K> : IGenericRepository<T, K> where T : class, IIdentity<K> where K : IComparable<K>
{

    protected readonly VideoGameDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public EFGenericRepository(VideoGameDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public void DisableChangeTracking()
    {
        _context.ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public void EnableChangeTracking()
    {
        _context.ChangeTracker.AutoDetectChangesEnabled = true;
    }
    public async Task<T> AddAsync(T o)
    {
        await _dbSet.AddAsync(o);
        return o;
    }

    public async Task AddRangeAsync(IEnumerable<T> o)
    {
        await _dbSet.AddRangeAsync(o);
    }

    public async Task<List<T>> FindAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T?> FindByIdAsync(K id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task RemoveAsync(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public IQueryable<T> FindBySpecification(ISpecification<T> specification = null)
    {
        IQueryable<T> query = _dbSet;

        if (specification is not null)
        {
            if (specification.Includes is not null)
            {
                foreach (var include in specification.Includes)
                {
                    query = query.Include(include);
                }
            }

            if (specification.Criteria is not null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.OrderBy is not null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }
        }

        return query;
    }
    public async Task<T> GetOrCreateAsync(Expression<Func<T, bool>> predicate, T newEntity)
    {
        var existingEntity = await _dbSet.FirstOrDefaultAsync(predicate);
        if (existingEntity != null)
        {
            return existingEntity;
        }

        _dbSet.Add(newEntity);
        await _context.SaveChangesAsync();
        return newEntity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
    }
}