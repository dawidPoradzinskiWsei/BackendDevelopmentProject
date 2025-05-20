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
    }
}