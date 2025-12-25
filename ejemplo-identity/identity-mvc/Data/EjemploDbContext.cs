using identity_mvc.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace identity_mvc.Data
{
    public class EjemploDbContext : IdentityDbContext
    {
        public EjemploDbContext(DbContextOptions<EjemploDbContext> options)
            : base(options)
        {
        }

        public DbSet<Auto> Autos { get; set; }
    }
}
