using ApplicationCore.Interfaces.UserService;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("games")]
        public ActionResult<IEnumerable<VideoGame>> GetAllGames()
        {
            var games = _userService.FindAllVideoGames();
            return Ok(games);
        }

        [HttpGet("game/{id}")]
        public ActionResult<VideoGame> GetGameById(int id)
        {
            var game = _userService.FindVideoGameById(id);
            if (game == null) return NotFound();
            return Ok(game);
        }

        [HttpGet("game/title/{title}")]
        public ActionResult<VideoGame> GetGameByTitle(string title)
        {
            var game = _userService.FindVideoGameByTitle(title);
            if (game == null) return NotFound();
            return Ok(game);
        }
    }
}