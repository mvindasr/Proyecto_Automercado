using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;

namespace Proyecto_Automercado
{
    [TestClass]
    public class PH04
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
        public void AutofrecuenteTest()
        {

            //Determinar variables de ventanas
            string username = "mvindasr@ucenfotec.ac.cr";
            string password = "autoTest1";
            string parentTab = driver.CurrentWindowHandle;
            string parentTabTitle = "Auto Mercado | Web";

            //Prerrequisito: iniciar sesión
            Utilities.Helpers.Login(driver, username, password);

            //Abrir el módulo de Autofrecuente
            Debug.WriteLine("Paso #1: Buscar y hacer clic al item del navbar de AutoFrecuente...");
            var autofrecuenteModule = Utilities.Helpers.GetByXPath(driver, "//am-main//am-home[@class='ng-star-inserted']/am-home-menu/nav//ul[@class='navbar-nav']//a[@href='/autofrecuente']");

            if (autofrecuenteModule == null)
            {
                Debug.WriteLine("Resultado #1: Falla, no se encuentra el ítem de Autofrecuente.");
            }
            else
            {
                autofrecuenteModule.Click();

                Debug.WriteLine("Resultado #1: Ingreso al módulo de reglamento AutoFrecuente exitoso.");
                Thread.Sleep(5000);
                Debug.WriteLine("Paso #2: Navegando al botón de descarga del reglamento y presionando...");

                // Se busca el botón para descargar el reglamento
             
                var regulationDownloadButton = Utilities.Helpers.GetByXPathWithDelay(driver, "/html/body/am-main//am-autofrecuente[@class='ng-star-inserted']//am-button[@title='Descargar reglamento']/a[@href='https://cms-strapi.azurewebsites.net/uploads/984e4ecedffc41bca056201f5cf767fa.pdf']", 10);
                
                //var regulationDownloadButton = driver.FindElement(By.XPath(("//am-main//am-autofrecuente[@class='ng-star-inserted']/am-home-menu/nav//ul[@class='navbar-nav']//a[@href='/proveedores']")));

                if (regulationDownloadButton == null)
                {
                    Debug.WriteLine("Resultado #2: Falla, no se encuentra el botón de descarga del reglamento...");
                }
                else
                {
                    Debug.WriteLine("Resultado #2: Se hizo clic en el botón de descarga de Reglamento AutoFrecuente.");
                    regulationDownloadButton.Click();
                    Debug.WriteLine("Paso #3: Detectando que nueva pestaña desde el módulo de AutoFrecuente fue abierta...");
                    Thread.Sleep(5000);

                    //Se detecta nueva pestaña
                    if (Utilities.Helpers.confirmNewTab(driver))
                    {
                        Debug.WriteLine("Resultado 3: Una nueva pestaña fue abierta desde el módulo.");

                        //Desplazarse a nueva pestaña para acceder al PDF

                        Debug.WriteLine("Paso #4: Verificando que la nueva pestaña contiene el archivo PDF de reglamento de AutoFrecuente...");
                        Utilities.Helpers.switchToNewGeneratedTab(driver, parentTab);
         

                        if (Utilities.Helpers.CompareData(parentTabTitle, driver.Title))
                        {
                            Debug.WriteLine("Resultado 4: Falla, el sistema se mantiene en la ventana padre '"+parentTabTitle+"'.");
                        }
                        else
                        {
                            Debug.WriteLine("Resultado 4: Nueva ventana con reglamento PDF detectada con éxito.");
                        }
                        Thread.Sleep(4000);
                    }
                    else
                    {
                        Debug.WriteLine("Resultado 3: No se abrió una nueva pestaña.");
                    }

                }
            }
        }
    

        [TestCleanup]
        public void Fin()
        {
            driver.Quit();
        }
    }
}
