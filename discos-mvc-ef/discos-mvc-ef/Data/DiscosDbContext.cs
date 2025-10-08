using Microsoft.EntityFrameworkCore;

namespace discos_mvc_ef.Data
{
    public class DiscosDbContext : DbContext
    {
        public DiscosDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<dominio.Disco> Discos { get; set; }
        public DbSet<dominio.Estilo> Estilos { get; set; }
        public DbSet<dominio.TipoEdicion> TiposEdicion { get; set; }
    }
}
