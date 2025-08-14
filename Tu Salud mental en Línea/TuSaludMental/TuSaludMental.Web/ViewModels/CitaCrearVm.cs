using System.ComponentModel.DataAnnotations;

namespace TuSaludMental.Web.ViewModels
{
    public class CitaCrearVm
    {
        [Required] public string Doctor { get; set; } = string.Empty;
        public string? Especialidad { get; set; }
        [Required] public DateTime FechaHora { get; set; }
        public string? Motivo { get; set; }
    }
}
