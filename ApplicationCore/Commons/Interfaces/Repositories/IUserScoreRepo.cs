public interface IUserScoreRepository
{
    Task AddAsync(UserScore userScore);
    Task<UserScore?> GetUserScoreAsync(int videoGameId, int userId);

    Task<IQueryable<UserScore>> GetAllUserScoresAsync(int videoGameId);
    Task RemoveAsync(UserScore userScore);
    Task UpdateAsync(UserScore userScore);
}