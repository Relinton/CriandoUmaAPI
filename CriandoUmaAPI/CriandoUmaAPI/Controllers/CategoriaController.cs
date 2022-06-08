using CriandoUmaAPI.Context;
using CriandoUmaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CriandoUmaAPI.Controllers
{
    [ApiController]
    [Route("v1/categorias")]
    public class CategoriaController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Categoria>>> Get([FromServices] APIContext context)
        {
            var categorias = await context.Categorias
                .ToListAsync();
            return categorias;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Categoria>> Post([FromServices] APIContext context, [FromBody] Categoria model)
        {
            if (ModelState.IsValid)
            {
                context.Categorias.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
