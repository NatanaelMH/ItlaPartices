using System.ComponentModel.DataAnnotations;

namespace SeniorCare.Application.Dtos.Juegos
{
    public class JuegoCreateDto
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Categoria { get; set; }
    }
}
