using ApplicationCore.Models;

namespace ApplicationCore.Commons.Repository;

public interface IVideoGameRepository : IGenericRepository<VideoGame, int>
{
    
    Task<IEnumerable<VideoGame>> GetByGenreAsync(string genreName);
    Task<IEnumerable<VideoGame>> GetByDeveloperAsync(string developerName);
    Task<IEnumerable<VideoGame>> GetByReleaseYearAsync(int year);
    Task<IEnumerable<VideoGame>> GetTopRatedGamesAsync(int count);
    Task<IEnumerable<VideoGame>> GetBySalesRangeAsync(decimal minSales, decimal maxSales);
}
