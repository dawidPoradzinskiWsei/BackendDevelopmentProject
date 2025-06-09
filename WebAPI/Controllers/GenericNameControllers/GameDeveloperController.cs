using ApplicationCore.Commons.Interfaces.Services.Developer;
using ApplicationCore.Commons.Models.Parts;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("v1/[controller]")]
public class GameDeveloperController : GenericController<GameDeveloper>
{
    public GameDeveloperController(IGenericNameService<GameDeveloper> service) : base(service)
    {
    }
}