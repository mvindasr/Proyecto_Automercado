using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Proyecto_Automercado.Utilities;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Proyecto_Automercado
{
    [TestClass]
    public class PF02
    {
        IWebDriver driver;
        [TestInitialize]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://automercado.cr");
            driver.Manage().Window.Maximize();
            // Esto es para que espere 5 segundos entre cada acción.
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            AddProductTest(driver);
        }

        [TestMethod]
        public void AddProductTest(IWebDriver driver)
        {
            // Constantes para contraseña y clave.
            string EMAIL = "tomatejuan@gmail.com";
            string PASS = "5Y5&QD6F9mzLHMm!";

            // Inicia sesión
            Helpers.Login(driver, EMAIL, PASS);
            Debug.WriteLine("Paso #1: Inicio de sesión...");

            // Busca los contenedores de las categorias de producto y los inserta en un arreglo.
            var AllCategoriesProduct = driver.FindElements(By.ClassName("slider-text"));
            

            Debug.WriteLine("Paso #2: Busca todas las categorías de producto.");
            for (int i = 0; i < AllCategoriesProduct.Count; i++)
            {
                // Despliega lista de las categorias.
                Debug.WriteLine(i + ". "+ AllCategoriesProduct[i].GetAttribute("innerText"));

           

                AllCategoriesProduct[i].Click();

            }


           


            /*
             * AllCategoriesProduct[17].Click();
            Helpers.GetByXPathWithDelay(driver, "//am-main//am-navbar[@class='ng-star-inserted']/nav//a[@href='/']/img", 10)?.Click();
            AllCategoriesProduct[0].Click(); */

            //driver.FindElements(By.TagName("span")).Where(elem => elem.Text.Trim() == "Nacionales").FirstOrDefault().Click();

            // Helpers.GetByXPathWithDelay(driver, "//am-main//am-navbar[@class='ng-star-inserted']/nav//a[@href='/']/img", 10)?.Click();

            // Utilities.Helpers.GetByXPathWithDelay(driver, "/html//div[@role='dialog']/div[@class='introjs-tooltipbuttons']/a[@role='button']", 10)?.Click();
            // Utilities.Helpers.GetByXPathWithDelay(driver, "/html//ngb-modal-window[@role='dialog']/div[@role='document']//app-onboarding//am-button/button[@type='button']", 10)?.Click();


            /*
            // Entra dentro de cada categoría de producto y vuelve con la siguiente.
            Debug.WriteLine("Paso #3: Click dentro de cada categoria.");
            for (int i = 0; i < AllCategoriesProduct.Count; i++)
            {
                
               Debug.WriteLine("Entro en la categorìa: " + AllCategoriesProduct[i].GetAttribute("innerText"));

     

                AllCategoriesProduct[i].Click();

                // driver.FindElement(By.LinkText("/content/images/logoAM.svg")).Click();

                Helpers.GetByXPathWithDelay(driver, "//am-main//am-navbar[@class='ng-star-inserted']/nav//a[@href='/']/img", 10)?.Click();

                // Cerrar modales
                 Utilities.Helpers.GetByXPathWithDelay(driver, "/html//div[@role='dialog']/div[@class='introjs-tooltipbuttons']/a[@role='button']", 10)?.Click();
                 Utilities.Helpers.GetByXPathWithDelay(driver, "/html//ngb-modal-window[@role='dialog']/div[@role='document']//app-onboarding//am-button/button[@type='button']", 10)?.Click();


            }
            
        }



   */


        }




        /*

          [TestCleanup]
      public void Fin()
          {
               driver.Quit();
          }
        */
    }

}