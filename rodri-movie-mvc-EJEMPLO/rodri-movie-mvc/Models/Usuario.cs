using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace rodri_movie_mvc.Models
{
    public class Usuario : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }
        public string ImagenUrlPerfil { get; set; }
        public List<Favorito>? PeliculasFavorito { get; set; }
        public List<Review>? ReviewsUsuario { get; set; }
    }
}
