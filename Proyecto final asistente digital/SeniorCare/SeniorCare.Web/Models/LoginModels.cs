namespace SeniorCare.Web.Models
{
    // Este modelo representa los datos que el usuario escribe en el formulario de login
    public class LoginModel
    {
        public string Email { get; set; }      // correo o usuario
        public string Password { get; set; }   // contraseña
    }
}
