using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Proyecto_Automercado.Utilities;

namespace Proyecto_Automercado
{
    [TestClass]
    public class PF03
    {
        IWebDriver driver;
        [TestInitialize]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://automercado.cr");
            driver.Manage().Window.Maximize();
        }

        [TestMethod]
        public void SponsorProducts()
        {
            if (Helpers.Login(driver, "jazroxsal@gmail.com", "Matías2901"))
            {
                //PASO 1 Click en la opción de "Coleccionables"
                OpenCollectiblesPage();

                Thread.Sleep(10000);

                //PASO 2 Desplegar los productos patrocinadores.
                SeeSponsorProducts();
            }
            else
            {
                Debug.WriteLine("Error al iniciar sesión.");
            }
            Thread.Sleep(10000);
        }

        public void OpenCollectiblesPage()
        {
            Debug.WriteLine("Paso #1: Hacer clic en el botón que dice \"Coleccionables\"");

            driver.FindElement(By.XPath("//am-main//am-home[@class='ng-star-inserted']/am-home-menu/nav//ul[@class='navbar-nav']//a[@href='/coleccionables']")).Click();

            Debug.WriteLine("Resultado #1: Se visualiza la página de coleccionables de Auto Mercardo");
        }

        public void SeeSponsorProducts()
        {
            Debug.WriteLine("Paso #2: Hacer clic en el botón que dice \"Patrocinadores\"");
            
            Helpers.GetByXPathWithDelay(driver, "//am-main//am-banner-collectibles[@class='ng-star-inserted']//am-menu-collectibles[@class='menu-collectibles pb-4']/nav/ul//a[@href='/coleccionables/patrocinadores']", 30)?.Click();

            Debug.WriteLine("Resultado #2: Se muestran los productos patrocinadores.");
        }

        [TestCleanup]
        public void Fin()
        {
            driver.Quit();
        }
    }
}
