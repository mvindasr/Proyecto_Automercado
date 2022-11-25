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

          
        }

      
        
        [TestMethod]
        public void AddProductTest()
        {
            // Constantes para contraseña y clave.
            string EMAIL = "tomatejuan@gmail.com";
            string PASS = "5Y5&QD6F9mzLHMm!";

            // Inicia sesión
            Helpers.Login(driver, EMAIL, PASS);
            Debug.WriteLine("Paso #1: Inicio de sesión...");
            Thread.Sleep(3000);
            
            // Busca los contenedores de las categorias de producto y los inserta en un arreglo.
            var AllCategoriesProduct = driver.FindElements(By.ClassName("slider-text"));
           
            List<string> categoryStrins = new List<string>();

            Debug.WriteLine("Contador del AllCategories" + AllCategoriesProduct.Count);

            
            Debug.WriteLine("Paso #2: Busca todas las categorías de producto.");
            for (int i = 0; i < AllCategoriesProduct.Count; i++)
            {

                
                // Despliega lista de las categorias.
                Debug.WriteLine(i + ". "+ AllCategoriesProduct[i].GetAttribute("innerText"));
                categoryStrins.Add(AllCategoriesProduct[i].GetAttribute("innerText")); 
            }

            Debug.WriteLine("Contador del categoryStrings: " + categoryStrins.Count);


            for (int i = 0; i < categoryStrins.Count; i++)
            {
                Debug.WriteLine(categoryStrins.ElementAt(i));

                Thread.Sleep(1000);
                var category = categoryStrins[i];

                // Da click a la categoría.
                driver.FindElement(By.XPath("//*[contains(text(), '" + category + "')]")).Click();
                
                //Revisa los productos por categoría.
                ProductReview(category);

                Thread.Sleep(3000);

                // Vuelve al home.
                var back = Helpers.GetByXPathWithDelay(driver, "//am-main//am-navbar[@class='ng-star-inserted']/nav//a[@href='/']/img", 2);
                if (back == null)
                {
                    Debug.WriteLine("No se encontro el elemento");
                }
                else
                {
                    back.Click();
                }

            }

        }


        public void ProductReview(string categoria)
        {

            Thread.Sleep(1000);
            // Busca los productos.
            var AllProducts = driver.FindElements(By.ClassName("title-product"));

            List<string> productsString = new List<string>();


            Debug.WriteLine("Paso #3: Busca todos los productos de una categoría.");
            for (int i = 0; i < 5; i++)
            {
                // Despliega lista de las productos.
               // Debug.WriteLine(i + ". " + AllProducts[i].GetAttribute("innerText"));
                productsString.Add(AllProducts[i].GetAttribute("innerText"));
            }

            Debug.WriteLine("Paso #5: Calcula cantidad de productos.");
            if (productsString.Count < 5)
            {
                Debug.WriteLine("Resultado #1: ");
                Debug.WriteLine("La categoria: " + categoria + ": No tiene almenos 5 productos.");
            }
            else
            {
                Debug.WriteLine("Resultado #2: ");
                Debug.WriteLine("La categoria: " + categoria + ": Si tiene almenos 5 productos.");
            }

        }

        [TestCleanup]
          public void Fin()
              {
                   driver.Quit();
              }
            
        }

}



