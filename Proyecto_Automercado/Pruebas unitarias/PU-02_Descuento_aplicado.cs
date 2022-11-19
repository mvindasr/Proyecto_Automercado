using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Proyecto_Automercado
{
    [TestClass]
    public class PU02
    {
        IWebDriver driver;
        [TestInitialize]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("");
            driver.Manage().Window.Maximize();
        }

        [TestMethod]
        public void Test1()
        {

            string var1 = "";
            string var2 = "";

            Assert.AreEqual(var1, var2);
        }

        [TestCleanup]
        public void Fin()
        {
            driver.Quit();
        }
    }
}
