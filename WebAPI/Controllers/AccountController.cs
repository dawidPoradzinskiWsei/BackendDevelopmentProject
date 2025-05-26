using ApplicationCore.Interfaces.AccountService;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            var (success, errors) = await _accountService.RegisterUserAsync(registerDto);

            if (!success)
                return BadRequest(new { Errors = errors });

            return Ok(new { Message = "User registered successfully" });
        }
    }
}