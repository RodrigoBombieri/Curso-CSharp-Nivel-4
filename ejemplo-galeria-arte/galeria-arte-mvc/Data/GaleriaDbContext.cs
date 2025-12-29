using galeria_arte_mvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace galeria_arte_mvc.Data
{
    public class GaleriaDbContext : IdentityDbContext
    {
        public GaleriaDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Artista> Artistas { get; set; }
        public DbSet<Obra> Obras { get; set; }
        public DbSet<Exposicion> Exposiciones { get; set; }

        // Configuración de la relación muchos a muchos entre Obra y Exposicion
        // Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Obra>()
                .HasMany(x => x.ExposicionesObra)
                .WithMany(x => x.ObrasExpuestas)
                .UsingEntity<Dictionary<string, object> > (
                    "ExposicionObra",
                    l => l.HasOne<Exposicion>()
                    .WithMany()
                    .HasForeignKey("ExposicionId")
                    .OnDelete(DeleteBehavior.Cascade),
                    r => r.HasOne<Obra>()
                    .WithMany()
                    .HasForeignKey("ObraId")
                    .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("ObraId", "ExposicionId");
                        j.ToTable("ExposicionObra");
                    });

            foreach (var fk in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
