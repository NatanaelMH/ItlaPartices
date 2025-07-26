using System;

namespace SeniorCare.Application.Dtos.Contactos
{
    public class ContactoUpdateDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Relacion { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }
}
