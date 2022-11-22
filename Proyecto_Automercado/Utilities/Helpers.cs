using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Automercado.Utilities
{
    internal class Helpers
    {

        public static bool Login(IWebDriver driver, string email, string password)
        {
            try
            {
                //abre el modal del login
                driver.FindElement(By.Id("login")).Click();

                //Busca el input de la email  y escribe el email
                var emailInput = driver.FindElement(By.Id("email"));
                emailInput.SendKeys(email);

                //Busca el input de la contraseña y escribe la contraseña
                var passwordInput = driver.FindElement(By.Id("password"));
                passwordInput.SendKeys(password);

                //Encuentra el botón de inicio de sesión y le da click. 
                driver.FindElement(
                    By.XPath("/html/body/ngb-modal-window[@role='dialog']/div[@role='document']//am-login-modal//" +
                    "form[@role='form']//am-button/button[@type='submit']")).Click();

                // Cerrar modales
                GetByXPathWithDelay(driver, "/html//div[@role='dialog']/div[@class='introjs-tooltipbuttons']/a[@role='button']", 10)?.Click();

                GetByXPathWithDelay(driver, "/html//ngb-modal-window[@role='dialog']/div[@role='document']//app-onboarding//am-button/button[@type='button']", 10)?.Click();
                return true;
            }
            catch 
            {
                return false;
            }
        }
        public static bool ElementExistsByPath(IWebDriver driver, string path)
        {
            try
            {
                driver.FindElement(By.XPath(path));
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static bool CompareData(string data1, string data2)
        {
            try
            {
                Assert.AreEqual(data1, data2);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static IWebElement? GetByXPathWithDelay(IWebDriver driver, string xPath, double seconds)
        {
            try
            {
                WebDriverWait wait = new(driver, TimeSpan.FromSeconds(seconds));
                return wait.Until(ExpectedConditions.ElementIsVisible((By.XPath((xPath)))));
            }
            catch
            {
                return null;
            }
        }

        public static IWebElement? GetByIdWithDelay(IWebDriver driver, string id, double seconds)
        {
            try
            {
                WebDriverWait wait = new(driver, TimeSpan.FromSeconds(seconds));
                return wait.Until(ExpectedConditions.ElementIsVisible((By.Id((id)))));
            }
            catch
            {
                return null;
            }
        }

        public static IWebElement? GetByXPath(IWebDriver driver, string xPath)
        {
            try
            {
                return driver.FindElement(By.XPath((xPath)));
            }
            catch
            {
                return null;
            }
        }
    }

    


}
