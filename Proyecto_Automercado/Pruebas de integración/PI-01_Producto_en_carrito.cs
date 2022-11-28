using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Proyecto_Automercado.Utilities;
using System.Diagnostics;

namespace Proyecto_Automercado
{
    [TestClass]
    public class PI01
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
            Debug.WriteLine("Test");
            if (Helpers.Login(driver, "feergranados@gmail.com", "autoTest123*"))
            {
                //PASO 1 ->  Agregar productos al carrito
                Debug.WriteLine("Sesión iniciada");
                AddProductsToCart();

                Thread.Sleep(10000);

                //PASO 2 -> Abrir Formulario de crear lista
                OpenForm();

                Thread.Sleep(20000);

                //PASO 3 -> Eliminar producto
                DeleteProduct();
               
            }
            else
            {
                Debug.WriteLine("Error al iniciar sesión.");
            }

        }

        public void AddProductsToCart()
        {
            var searchInput = driver.FindElement(By.XPath("/html//am-main//am-navbar[@class='ng-star-inserted']/nav//ul[@class='navbar-nav row w-100']//am-product-search-navbar/form//input[@type='search']"));

            //Buscar el producto
            searchInput.SendKeys("Shampoo Dove");
            searchInput.SendKeys(Keys.Enter);
            Debug.WriteLine("Paso #1: Buscando producto");
            Thread.Sleep(10000);
            //Agregar el producto
            driver.FindElement(By.XPath("/html/body/am-main//am-product-search[@class='ng-star-inserted']/div[@class='list']/div[@class='container']/div/div[@class='col-12 col-lg-10']//div[@class='grid-square']/div[1]/am-product-list//div[@class='card card-product']//am-product-button//button[@type='button']")).Click();
            Debug.WriteLine("Click en qué elemento?");
            
        }

        public void OpenForm()
        {
            //abre el carrito
            driver.FindElement(By.XPath("/html//am-main//am-navbar[@class='ng-star-inserted']/nav//ul[@class='navbar-nav row w-100']//am-shopping-cart/div")).Click();
            Debug.WriteLine("Paso #3: Abre el carrito");
            Thread.Sleep(10000);
           
        }             

        public void DeleteProduct() {

            //Botón eliminar
            driver.FindElement(By.XPath("/html/body/am-main//am-navbar[@class='ng-star-inserted']/nav//ul[@class='navbar-nav row w-100']//am-shopping-cart/form/div//a[@href='javascript:;']/i")).Click();
            Debug.WriteLine("Paso #5: Se elimina el producto del carrito");
        }

        [TestCleanup]
        public void Fin()
        {
            driver.Quit();
        }
    }
}
