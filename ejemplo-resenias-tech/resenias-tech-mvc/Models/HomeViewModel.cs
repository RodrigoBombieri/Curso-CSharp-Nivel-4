namespace resenias_tech_mvc.Models
{
    public class HomeViewModel
    {
        public List<Articulo> Articulos { get; set; } = new();
        public int TotalArticulos { get; set; }
    }
}
