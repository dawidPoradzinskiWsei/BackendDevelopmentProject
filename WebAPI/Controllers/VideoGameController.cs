using System.IdentityModel.Tokens.Jwt;
using ApplicationCore.Commons.Interfaces.Services.VideoGame.Admin;
using ApplicationCore.Commons.Models;
using ApplicationCore.Dto.Request.VideoGame;
using ApplicationCore.Dto.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
[ApiController]
[Route("/v1/[controller]")]
[SwaggerTag("2")]
public class VideoGameController : ControllerBase
{

    private readonly IUserVideoGameService _userService;
    private readonly IAdminVideoGameService _adminService;
    private readonly IVideoGameCsvImportService _csvService;

    public VideoGameController(IUserVideoGameService userService, IAdminVideoGameService adminService, IVideoGameCsvImportService csvService)
    {
        _userService = userService;
        _adminService = adminService;
        _csvService = csvService;
    }

    [HttpPost]
    [Route("upload")]
    [Authorize(Roles = "ADMIN")]
    [SwaggerOperation(Summary = "Upload video games to database", Description = "Upload a CSV file containing video game data. Need JWT")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(CsvImportResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UploadCsv(IFormFile file)
    {
        if (file is null || file.Length == 0)
        {
            return BadRequest();
        }

        var count = await _csvService.UploadCsvAsync(file.OpenReadStream());

        return Ok(new CsvImportResponseDto
        {
            Message = $"Added {count} games"
        });
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [SwaggerOperation(Summary = "Add a new video game", Description = "Add a new video game to the database. Need JWT")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(VideoGameResponseDto), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VideoGameResponseDto>> AddAsync([FromBody] VideoGameRequestDto game)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _adminService.AddVideoGameAsync(game);
        return Accepted(result);
    }

    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get a video game by ID", Description = "Retrieve a video game from the database by its ID")]
    [ProducesResponseType(typeof(VideoGameResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VideoGameResponseDto>> GetByIdAsync(int id)
    {
        var result = await _userService.GetVideoGameDtoByIdAsync(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all video games", Description = "Retrieve all video games from the database")]
    [ProducesResponseType(typeof(PagedList<VideoGameResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedList<VideoGameResponseDto>>> GetAllAsync([FromQuery] VideoGameParameters parameters)
    {
        var result = await _userService.GetFilteredAsync(parameters);

        var metadata = new
        {
            result.TotalCount,
            result.PageSize,
            result.CurrentPage,
            result.TotalPages,
            result.HasNext,
            result.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        return Ok(result);
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = "ADMIN")]
    [SwaggerOperation(Summary = "Update a video game by ID", Description = "Update an existing video game in the database. Need JWT")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(VideoGameResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VideoGameResponseDto>> UpdateAsync(int id, [FromBody] VideoGameRequestDto game)
    {

        try
        {
            var updatedGame = await _adminService.UpdateByIdAsync(id, game);
            return Ok(updatedGame);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "ADMIN")]
    [SwaggerOperation(Summary = "Delete a video game by ID", Description = "Delete an existing video game from the database. Need JWT")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await _adminService.DeleteByIdAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

    }

    [HttpPost]
    [Authorize]
    [Route("{id}/score")]
    [SwaggerOperation(Summary = "Add a score to a video game", Description = "Add a score for a video game by its ID. Need JWT")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(UserScore), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserScore>> AddScoreAsync(int id, [FromBody] AddUserScoreDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userIdClaim = User.FindFirst("userId");

        var userId = int.Parse(userIdClaim.Value);

        try
        {
            var result = await _userService.AddUserScoreAsync(id, userId, dto.Score);
            return Accepted(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException)
        {
            return BadRequest("You have already added a score for this game.");
        }



    }

    [HttpGet]
    [Route("{id}/score")]
    [Authorize]
    [SwaggerOperation(Summary = "Get a user's score for a video game", Description = "Retrieve the score a user has given to a video game by its ID. Need JWT")]
    [ProducesResponseType(typeof(UserScore), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserScore>> GetUserScoreAsync(int id)
    {
        var userIdClaim = User.FindFirst("userId");

        var userId = int.Parse(userIdClaim.Value);


        var score = await _userService.GetUserScoreAsync(id, userId);

        if (score is null)
        {
            return NotFound();
        }

        return Ok(score);
            
    }

    [HttpGet]
    [Route("{id}/score/all")]
    [SwaggerOperation(Summary = "Get all scores for a video game", Description = "Retrieve all scores given by users for a video game by its IDT")]
    [ProducesResponseType(typeof(PagedList<UserScore>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedList<UserScore>>> GetAllUserScoresAsync(int id, [FromQuery] PaginationParameters parameters)
    {
        var scores = await _userService.GetAllUserScoresAsync(id, parameters);

        var metadata = new
        {
            scores.TotalCount,
            scores.PageSize,
            scores.CurrentPage,
            scores.TotalPages,
            scores.HasNext,
            scores.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        return Ok(scores);
    }

    [HttpPut]
    [Route("{id}/score")]
    [Authorize]
    [SwaggerOperation(Summary = "Update a user's score for a video game", Description = "Update the score a user has given to a video game by its ID. Need JWT")]
    [Consumes("application/json")]
    [ProducesResponseType(typeof(UserScore), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserScore>> UpdateUserScoreAsync(int id, [FromBody] AddUserScoreDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var userIdClaim = User.FindFirst("userId");
        var userId = int.Parse(userIdClaim.Value);

        try
        {
            var updatedScore = await _userService.UpdateUserScoreAsync(id, userId, dto.Score);
            return Ok(updatedScore);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete]
    [Route("{id}/score")]
    [Authorize]
    [SwaggerOperation(Summary = "Delete a user's score for a video game", Description = "Delete the score a user has given to a video game by its ID. Need JWT")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUserScoreAsync(int id)
    {
        var userIdClaim = User.FindFirst("userId");
        var userId = int.Parse(userIdClaim.Value);

        try
        {
            await _userService.DeleteUserScoreAsync(id, userId);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}