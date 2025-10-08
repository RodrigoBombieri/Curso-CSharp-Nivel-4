using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Disco
    {
        public int Id { get; set; }
        public string Titulo { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Lanzamiento")]
        public DateTime FechaLanzamiento { get; set; }

        [Range (1, int.MaxValue, ErrorMessage = "La cantidad de canciones debe ser al menos 1.")]
        public int CantidadCanciones { get; set; }
        public string UrlTapa { get; set; }

        public int EstiloId { get; set; }
        public Estilo? Estilo { get; set; }
        public int TipoEdicionId { get; set; }
        public TipoEdicion? TipoEdicion { get; set; }
    }
}
