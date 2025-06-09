using ApplicationCore.Commons.Interfaces.Services.VideoGame.Admin;
using ApplicationCore.Commons.Interfaces.Services.VideoGame.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
[ApiController]
[Route("/v1/[controller]")]
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
    public async Task<IActionResult> UploadCsv([FromForm] IFormFile file)
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
    
    
}