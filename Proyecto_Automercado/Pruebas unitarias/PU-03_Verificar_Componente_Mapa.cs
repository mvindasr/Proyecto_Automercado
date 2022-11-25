using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Proyecto_Automercado.Utilities;
using System.Diagnostics;

namespace Proyecto_Automercado
{
    [TestClass]
    public class PU03
    {
        IWebDriver driver;
        [TestInitialize]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://automercado.cr");
            driver.Manage().Window.Maximize();
        }

        int flagTest = 0;

        [TestMethod]
        public void openMap()
        {
            InitSession();
            Debug.WriteLine("Paso #2: Se abre panel principal para seleccionar método de cobertura.");
            OpenMethodsPanel();
            OpenDomicilePanel();
            Debug.WriteLine("Paso #5: Se abre panel principal para seleccionar método de cobertura.");
            OpenMethodsPanel();
            OpenExpress();
            testFinalResult();
        }

      

        private void InitSession()
        {

            // Constantes para contraseña y clave.
            string EMAIL = "tomatejuan@gmail.com";
            string PASS = "5Y5&QD6F9mzLHMm!";

            // Se inicia sesión.
            if (Helpers.Login(driver, EMAIL, PASS))
            {

            }
            else
            {
                Debug.WriteLine("Error al iniciar sesión.");
            }
            Thread.Sleep(2000);

            Debug.WriteLine("Paso #1: Inicia sessión.");
        }


        private void OpenMethodsPanel()
        {

            // Abre el panel principal de métodos

            // Activa el dropdown.
            var btnGeneral = driver.FindElement(By.XPath("/html//button[@id='dropdownManual']"));
            Thread.Sleep(1000);
            btnGeneral.Click();

            Thread.Sleep(1000);

            // Selecciona abrir el panel general.
            var btnMethodsPanel = driver.FindElement(By.XPath("/html//am-main//am-navbar[@class='ng-star-inserted']/nav//ul[@class='navbar-nav row w-100']//li[@class='nav-item p-0']/div/div/button[.=' Otro método de compra ']"));
            Thread.Sleep(1000);
            btnMethodsPanel.Click();
            ;
            
        }

        private void OpenDomicilePanel()
        {
            // Abre el panel de domicilios.

            var select = Helpers.GetByXPathWithDelay(driver, "/html/body/ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-select-method/div[@class='modal-delivery-methods']//div[@class='container-fluid pt-2']/div[@class='row']/div[1]/am-method-card//am-button[@class='ng-star-inserted']/button[@type='button']", 10);
            select.Click();

            Debug.WriteLine("Paso #3: Se abre panel Domicilio.");

            Thread.Sleep(3000);

            // Se agrega ubicación
            var btnAddUbi = Helpers.GetByXPathWithDelay(driver, "/html//ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-select-address//a[@href='javascript:void(0)']", 10);
            btnAddUbi.Click();


            Debug.WriteLine("Paso #4: Se verifica si el mapa se abrio en el panel de Domicilio.");
            // Verifica si el mapa se desplego.
            mapIsON("Domicilio");
           

            // Se regresa a principal.
            var goBack = Helpers.GetByXPathWithDelay(driver, "/html//ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-select-address-map//a[@href='javascript:void(0);']/b[@class='green-text']", 10);
            goBack.Click();

        }

        private void mapIsON(string modulo)
        {

            string map = Convert.ToString(driver.FindElement(By.ClassName("gm-style-moc")));
           

            Debug.WriteLine("Este es el ID del:" + map);

            if (String.IsNullOrEmpty(map) != true)
            {
                Debug.WriteLine("Resultado #1: el mapa se desplego en el módulo: " + modulo);
                flagTest = flagTest + 1;

            }
            else
            {
                Debug.WriteLine("Resultado #2: el mapa NO se desplego en el módulo:" + modulo);
            }

        }


        private void OpenExpress()
        {
            // Abre el panel de Express.
            var select = Helpers.GetByXPathWithDelay(driver, "/html/body/ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-select-method/div[@class='modal-delivery-methods']//div[@class='container-fluid pt-2']/div[@class='row']/div[2]/am-method-card//am-button[@class='ng-star-inserted']/button[@type='button']", 10);
            select.Click();

            Debug.WriteLine("Paso #6: Se abre panel Express.");

            Thread.Sleep(4000);


            // Se agrega ubicación
            var btnAddUbi = Helpers.GetByXPathWithDelay(driver, "/html//ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-select-address//a[@href='javascript:void(0)']", 10);
            btnAddUbi.Click();


            Debug.WriteLine("Paso #7: Se verifica si el mapa se abrio en el panel de Express.");
            // Verifica si el mapa se desplego.
            mapIsON("Express");


            // Se regresa a principal.
            var goBack = Helpers.GetByXPathWithDelay(driver, "/html//ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-select-address-map//a[@href='javascript:void(0);']/b[@class='green-text']", 10);
            goBack.Click();

          

           
        }


        private void testFinalResult()
        {
            Debug.WriteLine("Paso #8: Se da el resultado de la prueba.");

            if ( flagTest == 2)
            {
                Debug.WriteLine("Resultado #1: El mapa si se desplego correctamente en los modúlos.");
            }
            else
            {
                Debug.WriteLine("Resultado #2: El mapa no se desplego correctamente en los modúlos.");
            }
        }


        [TestCleanup]
        public void Fin()
        {
            driver.Quit();
        }
    }
}

