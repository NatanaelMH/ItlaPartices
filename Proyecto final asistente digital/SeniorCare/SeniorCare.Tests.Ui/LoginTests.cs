using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;

namespace SeniorCare.Tests.Ui
{
    [TestClass]
    public class LoginTests : TestBase
    {
        // Ajusta el puerto si el tuyo es otro (mira el navegador)
        private const string BaseUrl = "https://localhost:44363";
        private const string LoginPath = "/Account/Login";

        [TestMethod]
        public void Login_CredencialesValidas_DeberiaEntrarAlHome()
        {
            // Navegar a la página de login
            driver.Navigate().GoToUrl(BaseUrl + LoginPath);

            // Buscar los campos por tipo, no por Id (más flexible)
            var emailInput = driver.FindElement(By.CssSelector("input[type='email'], input[name*='Email'], input[id*='Email']"));
            var passwordInput = driver.FindElement(By.CssSelector("input[type='password'], input[name*='Password'], input[id*='Password']"));

            emailInput.Clear();
            emailInput.SendKeys("admin@seniorcare.com");

            passwordInput.Clear();
            passwordInput.SendKeys("1234");

            // Botón de login: primer botón submit del formulario
            var loginButton = driver.FindElement(By.CssSelector("form button[type='submit'], form input[type='submit']"));
            loginButton.Click();

            // Comprobar que estamos en la pantalla principal
            var bodyText = driver.FindElement(By.TagName("body")).Text;

            Assert.IsTrue(
                bodyText.Contains("Pacientes") &&
                bodyText.Contains("Citas") &&
                bodyText.Contains("Juegos"),
                "No se mostraron las tarjetas de inicio tras un login válido."
            );
        }

        [TestMethod]
        public void Login_CredencialesInvalidas_DeberiaMostrarMensajeDeError()
        {
            driver.Navigate().GoToUrl(BaseUrl + LoginPath);

            var emailInput = driver.FindElement(By.CssSelector("input[type='email'], input[name*='Email'], input[id*='Email']"));
            var passwordInput = driver.FindElement(By.CssSelector("input[type='password'], input[name*='Password'], input[id*='Password']"));

            emailInput.Clear();
            emailInput.SendKeys("usuario@falso.com");

            passwordInput.Clear();
            passwordInput.SendKeys("mala");

            var loginButton = driver.FindElement(By.CssSelector("form button[type='submit'], form input[type='submit']"));
            loginButton.Click();

            var bodyText = driver.FindElement(By.TagName("body")).Text;

            Assert.IsTrue(
                bodyText.Contains("Credenciales incorrectas") ||
                bodyText.Contains("usuario o contraseña no válidos") ||
                bodyText.Contains("error"),
                "No se mostró un mensaje de error para credenciales inválidas."
            );
        }

        [TestMethod]
        public void Login_CamposVacios_NoDebePermitirEntrar()
        {
            driver.Navigate().GoToUrl(BaseUrl + LoginPath);

            var loginButton = driver.FindElement(By.CssSelector("form button[type='submit'], form input[type='submit']"));
            loginButton.Click();

            var bodyText = driver.FindElement(By.TagName("body")).Text;

            Assert.IsTrue(
                bodyText.Contains("obligatorio") ||
                bodyText.Contains("requerido") ||
                bodyText.Contains("Credenciales incorrectas") ||
                bodyText.Contains("error"),
                "El sistema permitió intentar login con campos vacíos sin mostrar validación."
            );
        }
    }
}
