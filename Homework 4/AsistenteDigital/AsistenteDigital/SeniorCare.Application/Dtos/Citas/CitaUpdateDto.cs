using System;
using System.ComponentModel.DataAnnotations;

namespace SeniorCare.Application.Dtos.Citas
{
    public class CitaUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Motivo { get; set; }

        [Required]
        public DateTime Fecha { get; set; }
    }
}
