namespace rodri_movie_mvc.Models
{
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string ImagenUrlPerfil { get; set; }

        public List<Favorito>? PeliculasFavorito { get; set; }
    }
}
