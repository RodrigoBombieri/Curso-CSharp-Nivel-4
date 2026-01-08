using System.ComponentModel.DataAnnotations;

namespace rodri_movie_mvc.Models
{
    public class Favorito
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public int PeliculaId { get; set; }
        public Pelicula? Pelicula { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
}
}
