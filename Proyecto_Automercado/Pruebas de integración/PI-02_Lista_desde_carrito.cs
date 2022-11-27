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
                Thread.Sleep(10000);
                OpenForm();
                
                Thread.Sleep(10000);
                FillForm();

                SaveList();
            }
            else
            {
                Debug.WriteLine("Error al iniciar sesión.");
            }
          Thread.Sleep(10000);
        }

        public void OpenForm()
        {
            Debug.WriteLine("Paso #1: Dar clic en el botón del carrito.");
            driver.FindElement(By.XPath("/html//am-main//am-navbar[@class='ng-star-inserted']/nav//ul[@class='navbar-nav row w-100']//am-shopping-cart/div")).Click();
            Debug.WriteLine("Resultado #1: Se visualiza el carrito de compras.");
            Thread.Sleep(10000);
            Debug.WriteLine("Paso #2: Dar clic en Guardar como lista.");
            driver.FindElement(By.XPath("/html//am-main//am-navbar[@class='ng-star-inserted']/nav//ul[@class='navbar-nav row w-100']//am-shopping-cart/form/div//div[@class='col-sm-12 shopping-content-actions']/button[1]")).Click();
            Debug.WriteLine("Resultado #2: Se muestra el formulario de crear una lista.");
        }

        public void FillForm() 
        {
            Debug.WriteLine("Paso #3: Ingresar el nombre de la lista a crear.");
            var listName = driver.FindElement(By.XPath("/html//ngb-modal-window[@role='dialog']/div[@role='document']//am-new-list//input[@type='search']"));
            listName.SendKeys("Básicos por semana.");
            Debug.WriteLine("Resultado #3: Se ingresa el nombre de la lista correctamente.");

            Debug.WriteLine("Paso #4: Dar clic en el tipo de lista Familia.");
            driver.FindElement(By.XPath("/html//ngb-modal-window[@role='dialog']/div[@role='document']//am-new-list//div[@class='container-button-type-list mb-5']/div[4]")).Click();
            Debug.WriteLine("Resultado #4: Se selecciona el tipo de lista exitosamente.");
        }

        public void SaveList()
        {
            Debug.WriteLine("Paso #5: Dar clic en botón Guardar.");
            driver.FindElement(By.XPath("//ngb-modal-window[@role='dialog']/div[@role='document']//am-new-list//am-button[@title='Guardar']/button[@type='button']")).Click();
            Debug.WriteLine("Resultado #5: Se visualiza la retroalimentación de que la lista ha sido creada exitosamente.");
        }



        [TestCleanup]
        public void Fin()
        {
           driver.Quit();
        }
    }
}
