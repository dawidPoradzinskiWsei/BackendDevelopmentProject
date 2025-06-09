using ApplicationCore.Commons.Interfaces.Services.Developer;
using ApplicationCore.Commons.Models.Parts;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("v1/[controller]")]
public class GameConsoleController : GenericController<GameConsole>
{
    public GameConsoleController(IGenericNameService<GameConsole> service) : base(service)
    {
    }
}