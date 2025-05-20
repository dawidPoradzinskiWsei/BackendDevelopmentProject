using ApplicationCore.Interfaces.AdminService;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")] // działa z JWT tokenem!
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("game")]
        public IActionResult AddGame([FromBody] VideoGame game)
        {
            var added = _adminService.AddVideoGame(game);
            return CreatedAtAction(nameof(AddGame), new { id = added.Id }, added);
        }

        [HttpPut("game/{id}")]
        public IActionResult UpdateGame(int id, [FromBody] VideoGame game)
        {
            _adminService.UpdateVideoGame(id, game);
            return NoContent();
        }

        [HttpDelete("game/{id}")]
        public IActionResult DeleteGame(int id)
        {
            _adminService.DeleteVideoGame(id);
            return NoContent();
        }
    }
}