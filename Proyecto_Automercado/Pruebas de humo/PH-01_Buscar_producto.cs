using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Proyecto_Automercado.Utilities;
using System.Diagnostics;

namespace Proyecto_Automercado
{
    [TestClass]
    public class PH01
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
        public void Test1()
        {
           
            if (Helpers.Login(driver, "feergranados@gmail.com", "autoTest123*"))
            {
                //PASO 1 ->  Agregar productos al carrito
                Debug.WriteLine("Sesión iniciada");
                SearchProduct();

                Thread.Sleep(10000);
            }
            else
            {
                Debug.WriteLine("Error al iniciar sesión.");
            }
        }

        public void SearchProduct()
        {
            var searchInput = driver.FindElement(By.XPath("/html//am-main//am-navbar[@class='ng-star-inserted']/nav//ul[@class='navbar-nav row w-100']//am-product-search-navbar/form//input[@type='search']"));

            //Buscar el producto
            searchInput.SendKeys("Helados");
            Debug.WriteLine("Paso #1: Ingresar el producto en la barra de búsqueda.");
            searchInput.SendKeys(Keys.Enter);
            Debug.WriteLine("Paso #2: Presionar click en el botón de búsqueda.");
            Thread.Sleep(10000);
        }

        [TestCleanup]
        public void Fin()
        {
            driver.Quit();
        }
    }
}
