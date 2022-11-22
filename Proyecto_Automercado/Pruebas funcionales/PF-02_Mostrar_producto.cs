using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

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
        public void loginProvicional()
        {
            driver.FindElement(By.Id("login")).Click();

            //Busca el input de la email  y escribe el email
            var emailInput = driver.FindElement(By.Id("email"));
            emailInput.SendKeys("tomatejuan@gmail.com");

            //Busca el input de la contraseña y escribe la contraseña
            var passwordInput = driver.FindElement(By.Id("password"));
            passwordInput.SendKeys("5Y5&QD6F9mzLHMm!");

            //Encuentra el botón de inicio de sesión y le da click. 
            driver.FindElement(
                By.XPath("/html/body/ngb-modal-window[@role='dialog']/div[@role='document']//am-login-modal//" +
                "form[@role='form']//am-button/button[@type='submit']")).Click();

            //Cerrar modales -- no funciona
            driver.FindElement(By.XPath("/body[@class='modal-open']/div[@class='introjs-overlay']")).Click();


       

        }

        public void Test1()
        {
            loginProvicional();

          var abarrotes = driver.FindElement(By.XPath("/html//am-main//am-home[@class='ng-star-inserted']/am-our-halls//section//div[@class='col-12']/div/div[1]/am-product-category//img[@alt='Abarrotes']"));
           abarrotes.Click();
            // get findelements con Strings, hacer ciclo en la lista para ver el producto. Con listas.

           // driver.FindElements(By.ClassName());
            var endulcorante = driver.FindElement(By.XPath("/html//am-main//am-product-search[@class='ng-star-inserted']/div[@class='list']/div[@class='container']/div/div[@class='col-12 col-lg-10']/div//div[@class='grid-square']/div[1]/am-product-list//div[@class='card card-product']//am-product-button//button[@type='button']"));
            endulcorante.Click();


            // string var1 = ""; 
            // string var2 = "";

            // Assert.AreEqual(var1, var2);
        }

        [TestCleanup]
        public void Fin()
        {
            // driver.Quit();
        }



    }
}
