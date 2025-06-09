using ApplicationCore.Commons.Interfaces.Services;
using ApplicationCore.Dto.Request.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("v1/[controller]")]
[SwaggerTag("1")]
public class AuthController : ControllerBase
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("register")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Register a new user",Description = "Allows a new user to register by providing the required details. Returns a token upon successful registration")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var token = await _authService.RegisterAsync(dto);
            return Ok(new { token });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new Dictionary<string, string>() { { "error", ex.Message } });
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [SwaggerOperation( Summary = "Authenticate a user", Description = "Authenticates a user by validating their credentials. Returns a token upon successful login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> Login(LoginDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var token = await _authService.LoginAsync(dto);
            return Ok(new { token });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (Exception)
        {
            return StatusCode(500);
        }
    }
}