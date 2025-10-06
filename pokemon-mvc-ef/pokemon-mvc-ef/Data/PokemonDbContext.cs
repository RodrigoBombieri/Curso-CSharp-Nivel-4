using pokemon_mvc_ef.Models;
using Microsoft.EntityFrameworkCore;
using dominio;

namespace pokemon_mvc_ef.Data
{
    public class PokemonDbContext : DbContext
    {
        public PokemonDbContext(DbContextOptions options) : base(options) {}
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<Elemento> Elementos { get; set; }

    }
}
