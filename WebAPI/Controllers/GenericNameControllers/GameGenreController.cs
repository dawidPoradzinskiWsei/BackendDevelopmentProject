using ApplicationCore.Commons.Interfaces.Services.Developer;
using ApplicationCore.Commons.Models.Parts;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("v1/[controller]")]
public class GameGenreController : GenericController<GameGenre>
{
    public GameGenreController(IGenericNameService<GameGenre> service) : base(service)
    {
    }
}