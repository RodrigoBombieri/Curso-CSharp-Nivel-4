namespace resenias_tech_mvc.Models
{
    public class Articulo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public float precio { get; set; }

        //public List<Resenia>? ArticuloResenias { get; set; }
    }
}
