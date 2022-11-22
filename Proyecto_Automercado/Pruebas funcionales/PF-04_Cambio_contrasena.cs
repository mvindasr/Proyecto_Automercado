using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Proyecto_Automercado
{
    [TestClass]
    public class PF04
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
        public void Test1()
        {

            string var1 = "hola";
            string var2 = "hola";

            Assert.AreEqual(var1, var2);
        }

        [TestCleanup]
        public void Fin()
        {
            driver.Quit();
        }
    }
}
