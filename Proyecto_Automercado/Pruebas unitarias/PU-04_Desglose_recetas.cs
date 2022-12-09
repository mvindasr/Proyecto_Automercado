using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;

namespace Proyecto_Automercado
{
    [TestClass]
    public class PU04
    {
        IWebDriver driver;
        [TestInitialize]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://automercado.cr");
            driver.Manage().Window.Maximize();
        }

        public void RecipeModuleAnalysis(Dictionary<string, string> recipeElements)
        {
            Debug.WriteLine("Paso #1: Ingresando al módulo de recetas...");

            var recipeModule = Utilities.Helpers.GetByXPathWithDelay(driver, "//am-main//am-home[@class='ng-star-inserted']/am-home-menu/nav//ul[@class='navbar-nav']//a[@href='/recetas']", 10);

            if (recipeModule == null)
            {
                Debug.WriteLine("Resultado #1: Falla, no se puede encontrar el módulo de recetas.");
            }
            else
            {
                recipeModule.Click();
                Thread.Sleep(3000);
                Debug.WriteLine("Resultado #1: Se ingresó al módulo de recetas exitosamente.");

                Debug.WriteLine("Paso #2: Analizando desglose de recetas según la categoría...");

                Boolean flag = false;
                foreach (var element in recipeElements)
                {
                    var categoryButton = Utilities.Helpers.GetByXPathWithDelay(driver, "/html//am-main//am-recipe[@class='ng-star-inserted']/div[@class='container recipe']//ul//span[.='" + element.Key + "']", 5);

                    if (categoryButton == null)
                    {
                        Debug.WriteLine("Resultado #2: Falla, no se encuentra la categoría "+element.Key+".");
                        break;
                    }
                    else
                    {
                        categoryButton.Click();
                        Thread.Sleep(3000);
                        var recipe = driver.FindElement(By.ClassName("card-recipe-title"));

                        if (recipe == null)
                        {
                            Debug.WriteLine("Resultado #2: Falla, no se encuentra la receta " + element.Value + ".");
                            break;
                        }
                        else
                        {
                            if (Utilities.Helpers.CompareData(element.Value, recipe.GetAttribute("title")))
                            {
                                flag = true;
                                Debug.WriteLine("- ¿Categoría '"+element.Key+"' contiene la receta '"+element.Value+"'? OK.");
                            }
                            else
                            {
                                Debug.WriteLine("- ¿Categoría '" + element.Key + "' contiene la receta '" + element.Value + "'? FALLA.");
                                break;
                            }
                            Thread.Sleep(1000);
                        }
                        
                    }
                }
                if (flag)
                {
                    Debug.WriteLine("Resultado #2: El módulo recetas desglosa categorías y su contenido exitosamente.");
                }
                else
                {
                    Debug.WriteLine("Resultado #2: Falla, el módulo recetas no desglosa contenido correctamente.");
                }

            }
        }

        [TestMethod]
        public void DesgloseRecetasTest()
        {

            Dictionary<string, string> recipeElements = new Dictionary<string, string>();

            recipeElements.Add("Postres", "Cheesecake de fresa y frambuesas sin azúcar");
            recipeElements.Add("Snacks", "Mug cake de banano");
            recipeElements.Add("Desayunos", "Bowl de desayuno");
            recipeElements.Add("Bebidas", "Milkshake de fresa");
            recipeElements.Add("Ensaladas", "Ensalada de orzo con pollo, tomate seco y espinaca");


            RecipeModuleAnalysis(recipeElements);
        }

        [TestCleanup]
        public void Fin()
        {
            driver.Quit();
        }
    }
}
