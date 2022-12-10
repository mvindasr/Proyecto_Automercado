using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Proyecto_Automercado.Utilities;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace Proyecto_Automercado
{
    [TestClass]
    public class PF01
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
        public void Test1()
        {
            string parentTab = driver.CurrentWindowHandle;

            Debug.WriteLine("Paso #1: Ir a Trabajá con nosotros");
            driver.FindElement(By.XPath("//am-main//am-home[@class='ng-star-inserted']/am-home-menu/nav//ul[@class='navbar-nav']//a[@href='/trabaja-con-nosotros']")).Click();
            Thread.Sleep(10000);

            Debug.WriteLine("Paso #2: Ir a Aplicar");
            driver.FindElement(By.XPath("/html/body/am-main//am-work-with-us[@class='ng-star-inserted']/div/div[4]//am-button[@title='Aplicar']/a[@href='https://empleo.automercado.cr']")).Click();
            Thread.Sleep(10000);

            Debug.WriteLine("Paso #3: Cambiar de pestaña");
            Utilities.Helpers.switchToNewGeneratedTab(driver, parentTab);
            Thread.Sleep(10000);

            Debug.WriteLine("Paso #4: Contar la cantidad de ofertas que carga");
            CountJobs();
            Thread.Sleep(10000);

            GoToFirstJob();
            Thread.Sleep(10000);

        }

        //Funcion para determinar la cantidad de empleos que despliega la pagina
        // imprime los que despliega
        // Indica si hay más o menos de 5 ofertas de empleo 
        public void CountJobs()
        {
            // Busca los contenedores de los empleos y los inserta en un arreglo.
            var AllJobs = driver.FindElements(By.ClassName("tc-job-row"));

            List<string> jobStrings = new List<string>();
            Debug.WriteLine("Paso #5: Busca todas los empleos.");
            for (int i = 0; i < AllJobs.Count; i++)
            {
                // Despliega lista de empleos
               // var jobOffer = jobStrings[i];               
                Debug.WriteLine(i + ". " + AllJobs[i].GetAttribute("innerText"));
                jobStrings.Add(AllJobs[i].GetAttribute("innerText"));
                //driver.FindElement(By.XPath("//*[contains(text(), '" + jobOffer + "')]")).Click();
            }


            Debug.WriteLine("Contador de ofertas de empleo: " + jobStrings.Count);
            
            if(jobStrings.Count > 5) {
                Debug.WriteLine("Resultado #4: El número de ofertas de empleo es mayor a 5 ");

            }
            else
            {
                Debug.WriteLine("Resultado #4: El número de ofertas de empleo es menor a 5");
            }
        }

        public void GoToFirstJob()
        {
            string parentTab = driver.CurrentWindowHandle;

            string XPathFirstJobOffer = "/html//div[@id='tc-jswidget']/div/div[@class='tc-jobs-container']//div[@class='tc-job-list']/div[1]/div[@class='tc-job-box3']/div[@class='tc-job-cell tc-job-insc']/a[@href='https://automercado.talentclue.com/es/node/92887424/4590']/span[.='Inscríbete']";

            string titleFirstOffer = driver.FindElement(By.XPath("/html//div[@id='tc-jswidget']/div/div[@class='tc-jobs-container']//div[@class='tc-job-list']/div[1]/div[@class='tc-job-box2']/div[@class='tc-job-cell tc-job-position']/a[@href='https://automercado.talentclue.com/es/node/92887424/4590']")).GetAttribute("innerText");
          
            Debug.WriteLine("Paso #6: Ir a la primera oferta de empleo");
            driver.FindElement(By.XPath(XPathFirstJobOffer)).Click();
           
            Debug.WriteLine("Paso #7: Cambiar de pestaña");
            Utilities.Helpers.switchToNewGeneratedTab(driver, parentTab);

            string jobTitle = driver.FindElement(By.XPath("//header[@id='header-section']//h1[.='Auxiliar Vindi - Campo Real']")).GetAttribute("innerText");
            
            if(Utilities.Helpers.CompareData(titleFirstOffer, jobTitle))
            {
                Debug.WriteLine("Paso #8: Presionar en el botón -Inscribirse-");
                Thread.Sleep(10000);
                driver.FindElement(By.XPath("/html//a[@id='buttons-social-buttons-legacy']")).Click();               
            }
            else
            {
                Debug.WriteLine("Resultado # : Los titulos no coinciden");
            }      

        }

        [TestCleanup]
        public void Fin()
        {
            driver.Quit();
        }
    }
}
