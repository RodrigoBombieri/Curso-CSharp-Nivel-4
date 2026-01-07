namespace rodri_movie_mvc.Models
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public int MinutosDuracion { get; set; }
        public string Sinopsis { get; set; }
        public string PosterUrlPortada { get; set; }
        public int GeneroId { get; set; }
        public Genero? Genero { get; set; }
        public int PlataformaId { get; set; }
        public Plataforma? Plataforma { get; set; }
        public  int PromedioRating { get; set; }
        public  List<Review>? ListaReviews { get; set; }
        public List<Favorito> UsuariosFavorito { get; set; }
    }
}
