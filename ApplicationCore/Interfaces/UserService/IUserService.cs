using ApplicationCore.Models;


namespace ApplicationCore.Interfaces.UserService
{
    public interface IUserService
    {
        IEnumerable<VideoGame> FindAllVideoGames();
        VideoGame? FindVideoGameById(int id);
        VideoGame? FindVideoGameByTitle(string title);
        
        VideoGame CreateVideoGame(VideoGame game);
        void UpdateVideoGame(int id, VideoGame game);
        void DeleteVideoGame(int id);
        
    }
    
    
}