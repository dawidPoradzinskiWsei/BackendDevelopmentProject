using ApplicationCore.Commons.Interfaces;
using ApplicationCore.Commons.Interfaces.Services.Developer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("v1/[controller]")]
public abstract class GenericController<T> : ControllerBase where T : NameEntity, new()
{
    private readonly IGenericNameService<T> _service;

    public GenericController(IGenericNameService<T> service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [SwaggerOperation(Summary = $"Create a new entity", Description = "Create a new entity with the specified name")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<T>> CreateAsync([FromBody] NameDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            return BadRequest("Name cannot be empty");
        }

        try
        {
            var createdEntity = await _service.CreateAsync(dto.Name);
            return Accepted(createdEntity);
        }
        catch (InvalidOperationException)
        {
            return Conflict();
        }


    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = $"Get an entity by ID", Description = "Retrieve a specific entity by its ID")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<T>> GetByIdAsync(int id)
    {
        var entity = await _service.GetByIdAsync(id);
        if (entity is null)
        {
            return NotFound();
        }
        return Ok(entity);
    }

    [HttpGet]
    [SwaggerOperation(Summary = $"Get all entity entities", Description = "Retrieve a paginated list of entities")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedList<T>>> GetAllAsync([FromQuery] NameParameters parameters)
    {
        var entities = await _service.GetFilteredAsync(parameters);

        var metadata = new
        {
            entities.TotalCount,
            entities.PageSize,
            entities.CurrentPage,
            entities.TotalPages,
            entities.HasNext,
            entities.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        return Ok(entities);
    }


    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN")]
    [SwaggerOperation(Summary = $"Update an entity by ID", Description = "Update the name of an existing entity. Need JWT")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] NameDto dto)
    {

        try
        {
            var updated = await _service.UpdateByIdAsync(id, dto.Name);
            return Ok(updated);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    [SwaggerOperation(Summary = $"Delete entity by ID", Description = "Delete an existing entity by its ID. Need JWT")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(int id)
    {

        try
        {
            var deleted = await _service.DeleteByIdAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
       
    }
}