namespace rodri_movie_mvc.Models
{
    public class Plataforma
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }
        public string LogoUrl { get; set; }
        public List<Pelicula>? PeliculasPlataforma { get; set; }
    }
}
