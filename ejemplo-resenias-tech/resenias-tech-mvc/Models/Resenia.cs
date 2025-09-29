namespace resenias_tech_mvc.Models
{
    public class Resenia
    {
        public int Id { get; set; }
        public Articulo Articulo { get; set; }
        public int Puntuacion { get; set; }
        public string Comentario { get; set; }
        public DateTime Fecha { get; set; }
    }
}
