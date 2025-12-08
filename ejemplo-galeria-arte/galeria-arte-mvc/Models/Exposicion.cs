using System.ComponentModel.DataAnnotations;

namespace galeria_arte_mvc.Models
{
    public class Exposicion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }
        [Display(Name = "Fecha de Fin")]
        public DateTime FechaFin { get; set; }
        public List<Obra>? ObrasExpuestas { get; set; }
    }
}
