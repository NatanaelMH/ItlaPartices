using System.ComponentModel.DataAnnotations;

namespace SeniorCare.Application.Dtos.Juegos
{
    public class JuegoUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Categoria { get; set; }
    }
}

