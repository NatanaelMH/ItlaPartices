namespace TuSaludMental.Web.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public int CitaId { get; set; }
        public string Canal { get; set; } = "EMAIL";
        public DateTime EnviarEl { get; set; }
        public DateTime? EnviadoEl { get; set; }
    }
}
