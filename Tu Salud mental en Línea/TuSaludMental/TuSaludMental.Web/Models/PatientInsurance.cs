namespace TuSaludMental.Web.Models
{
    public class PatientInsurance
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public int SeguroId { get; set; }
        public string NumeroPoliza { get; set; } = string.Empty;
        public DateOnly VigenteHasta { get; set; }
        public string Estado { get; set; } = "ACTIVO"; // ACTIVO / INACTIVO

        // 🔹 Navegación (necesaria para Include(x => x.Seguro) y para las vistas)
        public Patient? Paciente { get; set; }
        public Insurance? Seguro { get; set; }
    }
}
