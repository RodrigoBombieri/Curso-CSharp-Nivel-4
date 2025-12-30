using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using resenias_tech_mvc.Models;

namespace resenias_tech_mvc.Data
{
    public class ReseniasDbContext : IdentityDbContext
    {
        public ReseniasDbContext(DbContextOptions options): base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Resenia> Resenias { get; set; }
    }
}
