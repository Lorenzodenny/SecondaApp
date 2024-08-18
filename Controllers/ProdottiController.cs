using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecondaApp.Abstract;
using SecondaApp.Model;

namespace SecondaApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProdottiController : ControllerBase
    {
        private readonly IProdottoService _prodottoService;

        public ProdottiController(IProdottoService prodottoService)
        {
            _prodottoService = prodottoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var prodotti = await _prodottoService.GetAllProdottiAsync();
            return Ok(prodotti);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var prodotto = await _prodottoService.GetProdottoByIdAsync(id);
            if (prodotto == null)
                return NotFound();

            return Ok(prodotto);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Prodotto prodotto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _prodottoService.AddProdottoAsync(prodotto);
            return CreatedAtAction(nameof(GetById), new { id = prodotto.Id }, prodotto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Prodotto prodotto)
        {
            if (id != prodotto.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _prodottoService.UpdateProdottoAsync(prodotto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _prodottoService.DeleteProdottoAsync(id);
            return NoContent();
        }
    }

}
