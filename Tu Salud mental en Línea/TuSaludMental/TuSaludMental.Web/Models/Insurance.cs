namespace TuSaludMental.Web.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateOnly? FechaNacimiento { get; set; }
        public string? TipoSangre { get; set; }
        public string? Telefono { get; set; }
        public string Perfil { get; set; } = "ADULTO"; // CRONICO/JOVEN/ADULTO
    }
}
