using System.Threading.Tasks;
using ApplicationCore.Commons.Interfaces.Services.Developer;
using ApplicationCore.Commons.Models.Parts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
[ApiController]
[Route("v1/[controller]")]
public class DeveloperController : ControllerBase
{
    private readonly IDeveloperPublisherService<GameDeveloper> _service;

    public DeveloperController(IDeveloperPublisherService<GameDeveloper> service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [SwaggerOperation(Summary = "Create a new developer. Need JWT", Description = "Create a new game developer with the provided name")]
    [ProducesResponseType(typeof(GameDeveloper), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<GameDeveloper>> Create(NameDto dto)
    {

        try
        {
            var result = await _service.CreateAsync(dto.Name);
            return CreatedAtAction(nameof(GetById), new {id = result.Id}, result);
        }
        catch (EntityAlreadyExistException)
        {
            return Conflict();
        }


    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all developers", Description = "Retrieve a paginated list of game developers based on the provided query parameters")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedList<GameDeveloper>>> GetAll([FromQuery] NameParameters parameters)
    {
        var pagedGames = await _service.GetFilteredAsync(parameters);

        var metadata = new
        {
            pagedGames.TotalCount,
            pagedGames.PageSize,
            pagedGames.CurrentPage,
            pagedGames.TotalPages,
            pagedGames.HasNext,
            pagedGames.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        
        return Ok(pagedGames);
    }

    [HttpGet]
    [Route("{id}")]
    [SwaggerOperation(Summary = "Get developer by ID", Description = "Retrieve a specific game developer by their unique ID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GameDeveloper>> GetById(int id)
    {
        try
        {
            return await _service.GetByIdAsync(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = "ADMIN")]
    [SwaggerOperation(Summary = "Update developer by ID. Need JWT", Description = "Update the name of a specific game developer by their unique ID")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Update(int id, NameDto dto)
    {
        try
        {
            await _service.UpdateByIdAsync(id, dto.Name);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "ADMIN")]
    [SwaggerOperation(Summary = "Delete developer by ID. Need JWT", Description = "Delete a specific game developer by their unique ID")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        try
        {
            await _service.DeleteByIdAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}