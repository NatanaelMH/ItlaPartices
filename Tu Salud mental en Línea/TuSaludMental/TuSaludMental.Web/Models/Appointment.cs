namespace TuSaludMental.Web.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string Doctor { get; set; } = string.Empty;
        public string? Especialidad { get; set; }
        public DateTime FechaHora { get; set; }
        public string? Motivo { get; set; }
        public string Estado { get; set; } = "PENDIENTE";
    }
}
