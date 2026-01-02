namespace resenias_tech_mvc.Models
{
    public class ArticuloReseniaViewModel
    {
        public Articulo Articulo { get; set; }
        public List<Resenia> Resenias { get; set; }

        public List<UsuarioVM> Usuarios { get; set; }
    }
}
