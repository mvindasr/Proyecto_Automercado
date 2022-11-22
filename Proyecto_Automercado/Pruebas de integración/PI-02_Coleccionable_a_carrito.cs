using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Automercado.Pruebas_de_integración
{
    [TestClass]
    public class PI02
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
