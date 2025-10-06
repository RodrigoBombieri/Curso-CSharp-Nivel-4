using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Pokemon
    {
        public int Id { get; set; }
        [DisplayName("Número")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        [DisplayName("Imágen")]
        public string UrlImagen { get; set; }
        public int? TipoId { get; set; }
        public Elemento Tipo { get; set; }
        public int? DebilidadId { get; set; }
        public Elemento Debilidad { get; set; }
        public bool Activo { get; set; }

    }
}
