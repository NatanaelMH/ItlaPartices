using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace SeniorCare.Tests.Ui
{
    public class TestBase
    {
        protected IWebDriver? driver;
        protected ExtentTest? test;
        protected static ExtentReports? _extent;

        public TestContext? TestContext { get; set; }

        // ======================
        //  ANTES DE CADA TEST
        // ======================
        [TestInitialize]
        public void Setup()
        {
            // 1. Inicializar el reporte UNA sola vez
            if (_extent == null)
            {
                try
                {
                    var reportsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
                    Directory.CreateDirectory(reportsDir);

                    var reportPath = Path.Combine(reportsDir, "Reporte_SeniorCare.html");

                    var htmlReporter = new ExtentHtmlReporter(reportPath);
                    _extent = new ExtentReports();
                    _extent.AttachReporter(htmlReporter);
                }
                catch
                {
                    // Si falla el reporte, no tumbamos las pruebas
                }
            }

            // 2. Crear el navegador
            var options = new ChromeOptions();
            // Si quieres que no se vea la ventana:
            // options.AddArgument("--headless=new");

            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // 3. Crear el nodo del reporte para este test
            if (_extent != null && TestContext != null)
            {
                test = _extent.CreateTest(TestContext.TestName);
            }
        }

        // ======================
        //   DESPUÉS DE CADA TEST
        // ======================
        [TestCleanup]
        public void TearDown()
        {
            try
            {
                var outcome = TestContext?.CurrentTestOutcome ?? UnitTestOutcome.Inconclusive;

                // Tomar screenshot siempre
                string screenshotPath = string.Empty;
                try
                {
                    screenshotPath = TomarScreenshot();
                }
                catch
                {
                    // Ignoramos error de screenshot
                }

                // Registrar resultado en el reporte
                if (test != null)
                {
                    switch (outcome)
                    {
                        case UnitTestOutcome.Passed:
                            test.Pass("Test passed correctamente.");
                            break;
                        case UnitTestOutcome.Failed:
                            test.Fail("Test failed.");
                            break;
                        default:
                            test.Skip($"Test con estado: {outcome}.");
                            break;
                    }

                    if (!string.IsNullOrEmpty(screenshotPath))
                    {
                        try
                        {
                            test.AddScreenCaptureFromPath(screenshotPath);
                        }
                        catch
                        {
                            // Si no se puede adjuntar, no rompemos nada
                        }
                    }
                }
            }
            catch
            {
                // Nunca dejamos que un error aquí tumbe MSTest
            }
            finally
            {
                // Cerrar navegador
                try
                {
                    driver?.Quit();
                }
                catch
                {
                }

                // Guardar el reporte en disco
                try
                {
                    _extent?.Flush();
                }
                catch
                {
                }
            }
        }

        // ======================
        //   UTILIDAD: SCREENSHOT
        // ======================
        private string TomarScreenshot()
        {
            try
            {
                if (driver == null)
                    return string.Empty;

                if (driver is not ITakesScreenshot takesScreenshot)
                    return string.Empty;

                var screenshotsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
                Directory.CreateDirectory(screenshotsDir);

                var testName = TestContext?.TestName ?? "Test";
                var fileName = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                var fullPath = Path.Combine(screenshotsDir, fileName);

                var screenshot = takesScreenshot.GetScreenshot();
                // En esta versión basta pasar solo la ruta
                screenshot.SaveAsFile(fullPath);

                return fullPath;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
