using ApplicationCore.Commons.Repository;
using ApplicationCore.Interfaces.AdminService;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IGenericRepository<VideoGame, int> _videoRepository;

        public AdminService(IGenericRepository<VideoGame, int> videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public VideoGame AddVideoGame(VideoGame game)
        {
            return _videoRepository.Add(game);
        }

        public void UpdateVideoGame(int id, VideoGame game)
        {
            _videoRepository.Update(id, game);
        }

        public void DeleteVideoGame(int id)
        {
            _videoRepository.RemoveById(id);
        }
    }
}