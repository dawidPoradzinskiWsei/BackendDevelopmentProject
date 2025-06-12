using Microsoft.EntityFrameworkCore;

public class UserScoreRepository : IUserScoreRepository
{
    private readonly VideoGameDbContext _dbContext;
    private readonly DbSet<UserScore> _dbSet;
    public UserScoreRepository(VideoGameDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<UserScore>();
    }

    public async Task AddAsync(UserScore userScore)
    {
        await _dbSet.AddAsync(userScore);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IQueryable<UserScore>> GetAllUserScoresAsync(int videoGameId)
    {
        return _dbSet.Where(X => X.VideoGameId == videoGameId);
    }

    public async Task<UserScore?> GetUserScoreAsync(int videoGameId, int userId)
    {
        return await _dbSet.FindAsync(videoGameId, userId);
    }

    public async Task RemoveAsync(UserScore userScore)
    {
        _dbSet.Remove(userScore);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserScore userScore)
    {
        _dbSet.Update(userScore);
        await _dbContext.SaveChangesAsync();
    }
}