using Microsoft.AspNetCore.Mvc;
using SeniorCare.Web.Models;

namespace SeniorCare.Web.Controllers
{
    // Este controlador se encargará de mostrar el formulario de login
    // y de procesar lo que el usuario escriba.
    public class AccountController : Controller
    {
        // Muestra la pantalla de login (GET /Account/Login)
        [HttpGet]
        public IActionResult Login()
        {
            // Simplemente devuelve la vista Login.cshtml
            return View();
        }

        // Recibe los datos enviados desde el formulario (POST /Account/Login)
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            // Aquí simulamos un usuario "admin" con contraseña "1234".
            // Más adelante podrías conectarlo a BD si quisieras.
            if (model.Email == "admin@seniorcare.com" && model.Password == "1234")
            {
                // Si las credenciales son correctas, lo enviamos a la página de inicio
                return RedirectToAction("Index", "Home");
            }

            // Si llegó aquí, las credenciales son incorrectas
            ViewBag.Error = "Credenciales incorrectas. Intente de nuevo.";

            // Devolvemos la misma vista, mostrando el mensaje de error
            return View(model);
        }
    }
}

