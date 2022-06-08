
using CriandoUmaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CriandoUmaAPI.Context
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}
