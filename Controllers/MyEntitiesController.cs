using Microsoft.AspNetCore.Mvc;
using SecondaApp.Abstract;
using SecondaApp.Model;
using System.Xml;

[Route("api/[controller]")]
[ApiController]
public class MyEntitiesController : ControllerBase
{
    private readonly IEntityService<Prodotto> _entityService;

    public MyEntitiesController(IEntityService<Prodotto> entityService)
    {
        _entityService = entityService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var entities = _entityService.GetAll();
        return Ok(entities);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var entity = _entityService.GetById(id);
        if (entity == null)
        {
            return NotFound();
        }
        return Ok(entity);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Prodotto entity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _entityService.Create(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Prodotto entity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != entity.Id)
        {
            return BadRequest();
        }

        _entityService.Update(entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _entityService.Delete(id);
        return NoContent();
    }
}
