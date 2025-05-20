using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.UserService
{
    public interface IUserService
    {
        IEnumerable<VideoGame> FindAllVideoGames();
        VideoGame? FindVideoGameById(int id);
        VideoGame? FindVideoGameByTitle(string title);
    }
}