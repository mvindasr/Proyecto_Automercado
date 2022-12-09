using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V105.Tracing;
using System.Collections.Generic;
using System.Diagnostics;

namespace Proyecto_Automercado
{
    [TestClass]
    public class PI04
    {
        IWebDriver driver;
        [TestInitialize]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://automercado.cr");
            driver.Manage().Window.Maximize();
        }

        public void RecipeIntegrationProcess(string category, string recipe, string videoName, List<string> ingredients)
        {


            Debug.WriteLine("Paso #1: Ingresando al módulo de recetas...");

            var recipeModule = Utilities.Helpers.GetByXPathWithDelay(driver, "//am-main//am-home[@class='ng-star-inserted']/am-home-menu/nav//ul[@class='navbar-nav']//a[@href='/recetas']", 10);

            if (recipeModule == null)
            {
                Debug.WriteLine("Resultado #1: Falla, no se puede encontrar el módulo de recetas.");
            }
            else
            {
                Debug.WriteLine("Resultado #1: Se ingresó al módulo de recetas exitosamente.");
                recipeModule.Click();
                Thread.Sleep(3000);
                Debug.WriteLine("Paso #2: Ingresando a la categoría de " + category + "...");

                var categoryButton = Utilities.Helpers.GetByXPathWithDelay(driver, "/html//am-main//am-recipe[@class='ng-star-inserted']/div[@class='container recipe']//ul//span[.='" + category + "']", 10);
                if (categoryButton == null)
                {
                    Debug.WriteLine("Resultado #2: Falla, no se puede encontrar la categoría " + category + " en el menú.");
                }
                else
                {
                    categoryButton.Click();
                    Thread.Sleep(3000);
                    Debug.WriteLine("Resultado #2: Se ingresó a la categoría " + category + " con éxito.");
                    Debug.WriteLine("Paso #3: Buscando la receta de " + recipe + "...");

                    var recipeOption = Utilities.Helpers.GetByXPathWithDelay(driver, "/html//am-main//am-recipe[@class='ng-star-inserted']/div[@class='container recipe']/div[2]/div/div/div[1]/am-recipe-card//a[@title='Pastel de carne con puré gratinado']/h3[.='" + recipe + "']", 10);

                    if (recipeOption == null)
                    {
                        Debug.WriteLine("Resultado #3: Falla, no se puede encontrar la receta " + recipe + " en las opciones.");
                    }
                    else
                    {
                        recipeOption.Click();
                        Thread.Sleep(3000);
                        Debug.WriteLine("Resultado #3: Se ingresó a la receta " + recipe + " con éxito.");
                        Debug.WriteLine("Paso #4: Verificando integración de componentes (Video de Youtube e ingredientes de receta)...");

                        // Busca los contenedores de los ingredientes de receta y los inserta en un arreglo.
                        var allIngredients = driver.FindElements(By.ClassName("title-product"));
                        Thread.Sleep(2000);

                        List<string> ingredientTitles = new List<string>();

                        Debug.WriteLine("- Ingredientes disponibles para la receta: " + allIngredients.Count);

                        for (int i = 0; i < allIngredients.Count; i++)
                        {
                            // Carga los títulos de los ingredientes.

                            ingredientTitles.Add(allIngredients[i].GetAttribute("title"));
                        }

                        Boolean flag = false;
                        foreach (string ingredient in ingredients)
                        {
                            flag = ingredientTitles.Contains(ingredient);
                            if (!flag)
                            {
                                break;
                            }
                        }

                        if (flag)
                        {
                            Debug.WriteLine("- ¿Los datos de prueba de ingredientes se encuentran dentro de las "+ allIngredients.Count + " opciones? OK.");
                            var videoOption = driver.FindElement(By.TagName("iframe"));

                            Thread.Sleep(2000);
                            if (videoOption == null)
                            {
                                Debug.WriteLine("Resultado #4: Falla, elemento de video de receta no encontrado.");
                            }
                            else
                            {
                                driver.SwitchTo().Frame(videoOption);
                                var videoDetails = Utilities.Helpers.GetByXPathWithDelay(driver, "/html//div[@id='movie_player']//div[@class='ytp-title-text']/a[@href='https://www.youtube.com/watch?v=AjIVFhxf2Ik']", 5);

                                if (videoDetails == null)
                                {
                                    Debug.WriteLine("Resultado #4: Falla, no se encuentra el título del video de Youtube.");
                                }
                                else
                                {
                                    if (!videoDetails.Text.Equals(videoName))
                                    {
                                        Debug.WriteLine("Resultado #4: Falla, se ha cargado un video de Youtube incorrecto.");
                                    }
                                    else
                                    {
                                        if (!videoDetails.Text.Equals(videoName))
                                        {
                                            Debug.WriteLine("Resultado #4: Falla, se ha cargado un video de Youtube incorrecto.");
                                        }
                                        else
                                        {
                                            Debug.WriteLine("- ¿El video de Youtube de la recetá es el correcto? OK.");
                                            Debug.WriteLine("Resultado #4: El componente de video de Youtube y de los ingredientes de receta fueron cargados correctamente.");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Debug.WriteLine("Resultado #4: Falla, ingredientes de receta incorrectos.");
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void RecipeTest()
        {

  
            string category = "Platos Fuertes";
            string recipe = "Pastel de carne con puré gratinado";
            string videoName = "Pastel de carne";
            List<string> ingredients = new List<string> { "Ajo Especial", "Apio", "Papa", "Sal", "Zanahoria" };

            RecipeIntegrationProcess(category, recipe, videoName, ingredients);

        }

        [TestCleanup]
        public void Fin()
        {
            driver.Quit();
        }
    }
}
