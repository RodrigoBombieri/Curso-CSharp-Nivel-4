using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace galeria_arte_mvc.Models
{
    public class Obra
    {
        public Guid Id { get; set; }
        [StringLength(100)]
        public string Titulo { get; set; }
        [StringLength(50)]
        public string Estilo { get; set; }
        [StringLength(250)]
        public string UrlImagen { get; set; }
        public Guid ArtistaID { get; set; }
        public Artista? Artista { get; set; }
        public List<Exposicion>? ExposicionesObra { get; set; }
    }
}
