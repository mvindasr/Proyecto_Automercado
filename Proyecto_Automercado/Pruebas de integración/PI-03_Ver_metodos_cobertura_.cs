using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Proyecto_Automercado.Utilities;
using System.Diagnostics;


namespace Proyecto_Automercado
{
    [TestClass]
    public class PI03
    {
        IWebDriver driver;
        [TestInitialize]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://automercado.cr");
            driver.Manage().Window.Maximize();
        }

        int numMethods = 0;

        [TestMethod]
        public void viewMethodsOfCoverageArea()
        {
            int numMethods = 0;
            InitSession();

            OpenMethodsPanel();
            OpenDomicile();

            OpenMethodsPanel();
            OpenExpress();

            OpenMethodsPanel();
            OpenPickUp();

            AreAllMethodsDisplayed();

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
            var btnGeneral =  driver.FindElement(By.XPath("/html//button[@id='dropdownManual']"));
            Thread.Sleep(1000);
            btnGeneral.Click();

            Thread.Sleep(1000);

            // Selecciona abrir el panel general.
            var btnMethodsPanel = driver.FindElement(By.XPath("/html//am-main//am-navbar[@class='ng-star-inserted']/nav//ul[@class='navbar-nav row w-100']//li[@class='nav-item p-0']/div/div/button[.=' Otro método de compra ']"));
            Thread.Sleep(1000);
            btnMethodsPanel.Click();
;
            Debug.WriteLine("Paso #2: Se abre panel principal para seleccionar método de cobertura.");
        }

        private void OpenDomicile()
        {
           // Abre el panel de domicilios.

            var select = Helpers.GetByXPathWithDelay(driver, "/html/body/ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-select-method/div[@class='modal-delivery-methods']//div[@class='container-fluid pt-2']/div[@class='row']/div[1]/am-method-card//am-button[@class='ng-star-inserted']/button[@type='button']", 10);
            select.Click();

            Debug.WriteLine("Paso #3: Se abre panel Domicilio.");

            Thread.Sleep(3000);

            // Se regresa a principal.
            var goBack = Helpers.GetByXPathWithDelay(driver, "/html//ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-select-address//a[@href='javascript:;']/b[@class='green-text']", 10);
            goBack.Click();

            // Suma la bandera del panel desplegado.
            numMethods = numMethods + 1;
            Debug.WriteLine("Paso #4: Se devuelve al home.");
        }

        private void OpenExpress()
        {
            // Abre el panel de Express.
            var select = Helpers.GetByXPathWithDelay(driver, "/html/body/ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-select-method/div[@class='modal-delivery-methods']//div[@class='container-fluid pt-2']/div[@class='row']/div[2]/am-method-card//am-button[@class='ng-star-inserted']/button[@type='button']", 10);
            select.Click();

            Debug.WriteLine("Paso #5: Se abre panel Express.");
          
            Thread.Sleep(4000);

            // Se regresa a principal.
            var goBack = Helpers.GetByXPathWithDelay(driver, "/html//ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-select-address//a[@href='javascript:;']/b[@class='green-text']", 10);
            goBack.Click();

            // Suma la bandera del panel desplegado.
            numMethods = numMethods + 1;

            Debug.WriteLine("Paso #6: Se devuelve al home.");
        }

        private void OpenPickUp()
        {
            // Abre el panel de Pick Up.
            var select = Helpers.GetByXPathWithDelay(driver, "/html/body/ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-select-method/div[@class='modal-delivery-methods']//div[@class='container-fluid pt-2']/div[@class='row']/div[3]/am-method-card//am-button[@class='ng-star-inserted']/button[@type='button']", 10);
            select.Click();

            Debug.WriteLine("Paso #7: Se abre panel de pedidos Pick Up.");

            Thread.Sleep(3000);

            // Se regresa a principal.
            var goBack = Helpers.GetByXPathWithDelay(driver, "/html//ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-pickup//a[@href='javascript:;']/b[@class='green-text']", 10);
            goBack.Click();

            Thread.Sleep(4000);

            // Suma la bandera del panel desplegado.
            numMethods = numMethods + 1;
            Debug.WriteLine("Paso #8: Se devuelve al home.");
        }

        private void AreAllMethodsDisplayed()
        {
            Debug.WriteLine("Paso #9: Se despliega los resultados de las pruebas.");
            // Evalua si la prueba fue correcta en su totalidad.
            if (numMethods == 3 )
            {
                Debug.WriteLine("Resultado #1: Todos los menús de métodos fueron desplegados correctamente.");
            }
            else
            {
                Debug.WriteLine("Resultado #2: No se desplegaron correctamente los menús de métodos.");
            }

        }

        [TestCleanup]
        public void Fin()
        {
           driver.Quit();
        }
    }
}
