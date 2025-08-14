using System.ComponentModel.DataAnnotations;

namespace TuSaludMental.Web.ViewModels
{
    public class SeguroAgregarVm
    {
        [Required] public int SeguroId { get; set; }
        [Required] public string NumeroPoliza { get; set; } = string.Empty;
        [Required] public DateOnly VigenteHasta { get; set; }
    }
}
