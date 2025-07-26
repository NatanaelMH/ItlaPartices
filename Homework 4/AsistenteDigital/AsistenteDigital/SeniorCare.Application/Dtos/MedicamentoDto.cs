using System;

namespace SeniorCare.Application.Dtos.Medicamentos
{
    public class MedicamentoDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Dosis { get; set; }
        public string Frecuencia { get; set; }
    }
}
