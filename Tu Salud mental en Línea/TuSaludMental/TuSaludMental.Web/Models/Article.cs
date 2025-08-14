namespace TuSaludMental.Web.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Resumen { get; set; }
        public string? Contenido { get; set; }
        public string? Tags { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
