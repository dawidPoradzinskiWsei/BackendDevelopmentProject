using ApplicationCore.Interfaces.UserService;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoGameController : ControllerBase
{
    private readonly IUserService _userService;

    public VideoGameController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<VideoGame>> GetAll()
    {
        var games = _userService.FindAllVideoGames();
        return Ok(games);
    }

    [HttpGet("{id}")]
    public ActionResult<VideoGame> GetById(int id)
    {
        var game = _userService.FindVideoGameById(id);
        if (game == null)
            return NotFound($"Gra o ID {id} nie została znaleziona.");

        return Ok(game);
    }

    [HttpGet("title/{title}")]
    public ActionResult<VideoGame> GetByTitle(string title)
    {
        var game = _userService.FindVideoGameByTitle(title);
        if (game == null)
            return NotFound($"Gra o tytule '{title}' nie została znaleziona.");

        return Ok(game);
    }
    [HttpPost]
    public ActionResult<VideoGame> Create([FromBody] VideoGame game)
    {
        var created = _userService.CreateVideoGame(game);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] VideoGame game)
    {
        var existing = _userService.FindVideoGameById(id);
        if (existing == null)
            return NotFound($"Gra o ID {id} nie została znaleziona.");

        _userService.UpdateVideoGame(id, game);
        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existing = _userService.FindVideoGameById(id);
        if (existing == null)
            return NotFound($"Gra o ID {id} nie została znaleziona.");

        _userService.DeleteVideoGame(id);
        return NoContent();
    }



}