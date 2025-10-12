using System.ComponentModel.DataAnnotations;

namespace galeria_arte_mvc.Models
{
    public class Artista
    {
        // Guid para identificar unívocamente a cada artista
        // Se diferecia de un entero autoincremental en que no puede ser adivinado o predecido
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El nombre del artista es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El país del artista es obligatorio")]
        [StringLength(100)]
        public string Pais { get; set; }
    }
}
