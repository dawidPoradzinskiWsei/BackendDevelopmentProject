using ApplicationCore.Commons.Interfaces.Services.Developer;
using ApplicationCore.Commons.Models.Parts;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("v1/[controller]")]
public class GamePublisherController : GenericController<GamePublisher>
{
    public GamePublisherController(IGenericNameService<GamePublisher> service) : base(service)
    {
    }
}