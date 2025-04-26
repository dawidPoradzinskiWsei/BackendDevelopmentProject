using ApplicationCore.Commons.Repository;
using ApplicationCore.Commons.Specification;
using Microsoft.EntityFrameworkCore;

public class EfGenericRepository<T, K> : IGenericRepository<T, K> where T : class, IIdentity<K> where K : IComparable<K>
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public EfGenericRepository(VideoGameDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    public T Add(T o)
    {
        _dbSet.Add(o);
        _context.SaveChanges();
        return o;
    }

    public List<T> FindAll()
    {
        return _dbSet.ToList();
    }

    public async Task<List<T>> FindAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public T? FindById(K id)
    {
        return _dbSet.FirstOrDefault(v => v.Id.CompareTo(id) == 0);
    }

    public async Task<T?> FindByIdAsync(K id)
    {
        return await _dbSet.FirstOrDefaultAsync(v => v.Id.CompareTo(id) == 0);
    }

    public IEnumerable<T> FindBySpecification(ISpecification<T> specification = null)
    {
        throw new NotImplementedException();
    }

    public void RemoveById(K id)
    {
        var entity = _dbSet.Find(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }

    public void Update(K id, T o)
    {
        var existing = _dbSet.FirstOrDefault(v => v.Id.CompareTo(id) == 0);
        if (existing != null)
        {
            _context.Entry(existing).CurrentValues.SetValues(o);
            _context.SaveChanges();
        }
    }
}