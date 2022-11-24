using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Proyecto_Automercado.Utilities;
using System.Diagnostics;

namespace Proyecto_Automercado.Pruebas_de_integración
{
    [TestClass]
    public class PI02
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

            if (Helpers.Login(driver, "jazroxsal@gmail.com", "Matías2901"))
            {
                //PASO 1 ->  Agregar productos al carrito
                AddProductsToCart();

                Thread.Sleep(10000);

                //PASO 2 -> Abrir Formulario de crear lista
                OpenForm();

                Thread.Sleep(20000);
                // PASO 3 -> Llenar el formulario
                FillForm();

                Thread.Sleep(10000);

                //PASO 4 -> Guardar la lista
                SaveList();
            }
            else
            {
                Debug.WriteLine("Error al iniciar sesión.");
            }
           // Thread.Sleep(10000);
        }

        public void AddProductsToCart()
        {
            var searchInput = driver.FindElement(By.XPath("/html//am-main//am-navbar[@class='ng-star-inserted']/nav//ul[@class='navbar-nav row w-100']//am-product-search-navbar/form//input[@type='search']"));

            //Buscar arroz Luisiana
            searchInput.SendKeys("Arroz Luisiana");
            searchInput.SendKeys(Keys.Enter);
            Thread.Sleep(10000);
            //Agregar el arroz al carrito
            driver.FindElement(By.XPath("/html/body/am-main//am-product-search[@class='ng-star-inserted']/div[@class='list']/div[@class='container']/div/div[@class='col-12 col-lg-10']//div[@class='grid-square']/div[1]/am-product-list//div[@class='card card-product']//am-product-button//button[@type='button']")).Click();

            
            Thread.Sleep(10000); 
            searchInput.Clear();
            //Buscar Frijoles don pedro
            searchInput.SendKeys("Frijoles Don Pedro");
            searchInput.SendKeys(Keys.Enter);
            Thread.Sleep(10000);
            //Agregar los frijoles al carrito
            driver.FindElement(By.XPath("/html/body/am-main//am-product-search[@class='ng-star-inserted']/div[@class='list']/div[@class='container']/div/div[@class='col-12 col-lg-10']//div[@class='grid-square']/div[2]/am-product-list//div[@class='card card-product']//am-product-button//button[@type='button']")).Click();

            Thread.Sleep(10000);
            searchInput.Clear();
            //Buscar Pollo asado
            searchInput.SendKeys("Pollo asado");
            searchInput.SendKeys(Keys.Enter);
            Thread.Sleep(10000);
            //Agregar los frijoles al carrito
            driver.FindElement(By.XPath("/html/body/am-main//am-product-search[@class='ng-star-inserted']/div[@class='list']/div[@class='container']/div/div[@class='col-12 col-lg-10']//div[@class='grid-square']/div[1]/am-product-list//div[@class='card card-product']//am-product-button//button[@type='button']")).Click();
        }

        public void OpenForm()
        {
            driver.FindElement(By.XPath("/html//am-main//am-navbar[@class='ng-star-inserted']/nav//ul[@class='navbar-nav row w-100']//am-shopping-cart/div")).Click();
            Thread.Sleep(10000);
            driver.FindElement(By.XPath("/html//am-main//am-navbar[@class='ng-star-inserted']/nav//ul[@class='navbar-nav row w-100']//am-shopping-cart/form/div//div[@class='col-sm-12 shopping-content-actions']/button[1]")).Click();
        }

        public void FillForm() 
        {
            //busca el input del nombre de la lista
            var listName = driver.FindElement(By.XPath("/html//ngb-modal-window[@role='dialog']/div[@role='document']//am-new-list//input[@type='search']"));
            listName.SendKeys("Básicos por semana.");

            //Selecciona el tipo de lista
            driver.FindElement(By.XPath("/html//ngb-modal-window[@role='dialog']/div[@role='document']//am-new-list//div[@class='container-button-type-list mb-5']/div[4]")).Click();
        }

        public void SaveList()
        {
            //Botón Guardar
            driver.FindElement(By.XPath("//ngb-modal-window[@role='dialog']/div[@role='document']//am-new-list//am-button[@title='Guardar']/button[@type='button']")).Click();
        }



        [TestCleanup]
        public void Fin()
        {
           // driver.Quit();
        }
    }
}
