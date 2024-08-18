﻿using Microsoft.AspNetCore.Mvc;
using SecondaApp.Abstract;
using System.Threading.Tasks;

namespace SecondaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdottoCategoriaViewController : ControllerBase
    {
        private readonly IProdottoCategoriaViewService _service;

        public ProdottoCategoriaViewController(IProdottoCategoriaViewService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }
    }
}
