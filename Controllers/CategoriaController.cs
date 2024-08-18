using Microsoft.AspNetCore.Mvc;
using SecondaApp.Abstract;
using SecondaApp.Model;

namespace SecondaApp.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Categoria categoria)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _categoriaService.AddCategoriaAsync(categoria);
            return Ok();
        }
    }
}
