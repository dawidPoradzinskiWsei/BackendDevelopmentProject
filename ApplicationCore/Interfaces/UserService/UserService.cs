using ApplicationCore.Commons.Repository;
using ApplicationCore.Interfaces.UserService;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.UserService
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<VideoGame, int> _videoRepository;

        public UserService(IGenericRepository<VideoGame, int> videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public IEnumerable<VideoGame> FindAllVideoGames()
        {
            return _videoRepository.FindAll();
        }

        public VideoGame? FindVideoGameById(int id)
        {
            return _videoRepository.FindById(id);
        }

        public VideoGame? FindVideoGameByTitle(string title)
        {
            return _videoRepository
                .FindAll()
                .FirstOrDefault(v => v.Title.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }
        public VideoGame CreateVideoGame(VideoGame game)
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