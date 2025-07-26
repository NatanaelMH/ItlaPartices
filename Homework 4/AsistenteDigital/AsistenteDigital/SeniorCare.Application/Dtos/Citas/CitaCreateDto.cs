using System;
using System.ComponentModel.DataAnnotations;

namespace SeniorCare.Application.Dtos.Citas
{
    public class CitaCreateDto
    {
        [Required]
        public string Motivo { get; set; }

        [Required]
        public DateTime Fecha { get; set; }
    }
}
