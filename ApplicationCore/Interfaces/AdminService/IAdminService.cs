using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.AdminService
{
    public interface IAdminService
    {
        VideoGame AddVideoGame(VideoGame game);
        void UpdateVideoGame(int id, VideoGame game);
        void DeleteVideoGame(int id);
    }
}