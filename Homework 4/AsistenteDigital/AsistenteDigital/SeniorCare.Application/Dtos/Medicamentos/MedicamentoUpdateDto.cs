using System;
using System.ComponentModel.DataAnnotations;

namespace SeniorCare.Application.Dtos.Medicamentos
{
    public class MedicamentoUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Dosis { get; set; }

        [Required]
        public DateTime Hora { get; set; }
    }
}
