using ApplicationCore.Commons.Repository;
using ApplicationCore.Models;

public class EfVideoGameRepository : EfGenericRepository<VideoGame, int>, IVideoGameRepository
{
    private readonly VideoGameDbContext _context;
    public EfVideoGameRepository(VideoGameDbContext context) : base(context)
    {
        _context = context;
    }

    public Task<IEnumerable<VideoGame>> GetByDeveloperAsync(string developerName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<VideoGame>> GetByGenreAsync(string genreName)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<VideoGame>> GetByReleaseYearAsync(int year)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<VideoGame>> GetBySalesRangeAsync(decimal minSales, decimal maxSales)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<VideoGame>> GetTopRatedGamesAsync(int count)
    {
        throw new NotImplementedException();
    }
}