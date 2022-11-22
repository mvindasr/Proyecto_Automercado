using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Diagnostics;
using Proyecto_Automercado.Utilities;

namespace Proyecto_Automercado
{
    [TestClass]
    public class LoginBasic
    {
        IWebDriver driver;
        [TestInitialize]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://automercado.cr/");
            driver.Manage().Window.Maximize();
        }

        [TestMethod]
        public void Login()
        {
            //Variables de prueba
            var setEmail = "mvindasr@ucenfotec.ac.cr";
            var setPassword = "autoTest1";

            if (Helpers.Login(driver, setEmail, setPassword))
            {
                Debug.WriteLine("Inicio de sesión exitosa.");
            }
            else
            {
                Debug.WriteLine("Fallo en el inicio de sesión.");
            }
            Thread.Sleep(10000);
        }

        [TestCleanup]
        public void Fin()
        {
            driver.Quit();
        }
    }
}

