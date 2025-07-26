using System;

namespace SeniorCare.Application.Dtos.Contactos
{
    public class ContactoCreateDto
    {
        public string Nombre { get; set; }
        public string Relacion { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
    }
}
