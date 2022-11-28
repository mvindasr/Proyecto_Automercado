using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Proyecto_Automercado.Utilities;


namespace Proyecto_Automercado
{
    [TestClass]
    public class PH02
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
        public void DeleteContact()
        {
            if (Helpers.Login(driver, "jazroxsal@gmail.com", "Matías2901"))
            {
                //Paso 1 Ir al perfil
                GoToProfile();

                //Paso 2 eliminar contacto
                Delete();

                Thread.Sleep(10000);
                //Paso 3 confirmar eliminado
                Confirm();
            }
            else
            {
                Debug.WriteLine("Error al iniciar sesión.");
            }

            Thread.Sleep(10000);
        }

        public void GoToProfile()
        {
            Debug.WriteLine("Paso #1: Ingresar al perfil.");
            //Clic en el botón que contiene el nombre
            driver.FindElement(By.XPath("/html//button[@id='dropdownManual']")).Click();
            //Clic en el botón que dice "Mi perfil"
            driver.FindElement(By.XPath("/html//am-main//am-navbar[@class='ng-star-inserted']/nav//ul[@class='navbar-nav row w-100']//li[@class='nav-item ng-star-inserted']/div/div/a[@href='/perfil']")).Click();
            //Cerrar modal
            Helpers.GetByXPathWithDelay(driver, "/html//div[@role='dialog']/div[@class='introjs-tooltip-header']/a[@role='button']", 10)?.Click();
            Debug.WriteLine("Resultado #1: Se muestra el perfil.");
        }

        public void Delete()
        {
            Debug.WriteLine("Paso #2: Dar clic en el botón que tiene el icono de basurero.");
            driver.FindElement(By.XPath("/html//div[@id='step-four']/div[@class='ng-star-inserted']/div[2]/am-address-card[1]//img[@src='/content/images/profile/trash.svg']")).Click();
            Debug.WriteLine("Resultado #2:  Se muestra el mensaje para confirmar que se desea eliminar el contacto.");
        }

        public void Confirm()
        {
            Debug.WriteLine("Paso #3: Dar clic en el botón que dice \"Ok\"");
            driver.FindElement(By.XPath("/html//div[@role='dialog']/div[@class='swal2-actions']/button[1]")).Click();
            Debug.WriteLine("Resultado #3: Desaparece el contacto eliminado y no muestra ninguna retroalimentación.");
        }

        [TestCleanup]
        public void Fin()
        {
            driver.Quit();
        }
    }
}
