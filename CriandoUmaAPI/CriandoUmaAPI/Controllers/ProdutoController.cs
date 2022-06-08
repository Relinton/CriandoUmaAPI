using CriandoUmaAPI.Context;
using CriandoUmaAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CriandoUmaAPI.Controllers
{
    [ApiController]
    [Route("v1/produtos")]
    public class ProdutoController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Produto>>> Get([FromServices] APIContext context)
        {
            var produtos = await context.Produtos
                .Include(x => x.Categoria)
                .ToListAsync();
            return produtos;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Produto>> Post([FromServices] APIContext context, [FromBody] Produto model)
        {
            if (ModelState.IsValid)
            {
                context.Produtos.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Produto>> GetById([FromServices] APIContext context, int id)
        {
            var produto = await context.Produtos.Include(x => x.Categoria)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return produto;
        }

        [HttpGet]
        [Route("categorias/{id:int}")]
        public async Task<ActionResult<List<Produto>>> GetByCategoria([FromServices] APIContext context, int id)
        {
            var produtos = await context.Produtos
                .Include(x => x.Categoria)
                .AsNoTracking()
                .Where(x => x.CategoriaId == id)
                .ToListAsync();
            return produtos;
        }
    }
}
