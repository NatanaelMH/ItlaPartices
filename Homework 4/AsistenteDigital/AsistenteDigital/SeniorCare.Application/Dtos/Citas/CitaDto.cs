using System;

namespace SeniorCare.Application.Dtos.Citas
{
    public class CitaDto
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }
        public string Medico { get; set; }
        public string Nombre { get; set; } // Solucionado aquí
    }
}
