using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;

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

        public void ChangePassword(string email, string oldPass, string newPass, string confirmPass)
        {

            //Prerrequisito: iniciar sesión
            Utilities.Helpers.Login(driver, email, oldPass);

            //Ingresar al perfil de usuario

            Utilities.Helpers.GetByXPathWithDelay(driver, "//button[@id='dropdownManual']/span[@class='pl-2']", 3)?.Click();
            Debug.WriteLine("Paso #1: Ingresando al módulo de Perfil de usuario a través del menú desplegable...");
            var profileModule = Utilities.Helpers.GetByXPathWithDelay(driver, "/html//am-main//am-navbar[@class='ng-star-inserted']/nav//ul[@class='navbar-nav row w-100']//li[@class='nav-item ng-star-inserted']/div/div/a[@href='/perfil']/span[@class='pl-2']", 5);

            if (profileModule == null)
            {
                Debug.WriteLine("Resultado #1: Falla, no se pudo acceder al módulo de Perfil de Usuario.");
            }
            else
            {
                profileModule.Click();
                Thread.Sleep(3000);
                Debug.WriteLine("Resultado #1: Ingreso al módulo de Perfil de Usuario exitoso.");

                // Buscar e ingresar al módulo de cambio de contraseña

                var passwordModule = Utilities.Helpers.GetByXPathWithDelay(driver, "/html/body/am-main//am-profile-layout[@class='ng-star-inserted']/am-me[@class='ng-star-inserted']/div[@class='container-fluid me-profile']//am-config//span[.='Cambiar mi contraseña']", 10);
                Debug.WriteLine("Paso #2: Ingresando al módulo de Cambio de Contraseña entre las opciones del perfil...");

                if (passwordModule == null)
                {
                    Debug.WriteLine("Resultado #2: Falla, no se pudo acceder al módulo de Cambio de Contraseña.");
                }
                else
                {

                    // Cerrar Pop-ups

                    Utilities.Helpers.GetByXPathWithDelay(driver, "/html//div[@role='dialog']/div[@class='introjs-tooltip-header']/a[@role='button']", 3)?.Click();
                    Thread.Sleep(3000);
                    // Ingresar datos del cambio y verificar su correctitud.
                    passwordModule.Click();
                    var oldPassField = driver.FindElement(By.Id("oldPassword"));
                    var newPassField = driver.FindElement(By.Id("newPassword"));
                    var confirmPassField = driver.FindElement(By.Id("confirmPassword"));

                    Debug.WriteLine("Resultado #2: Se abrió el módulo de cambio de contraseña exitosamente.");

                    Debug.WriteLine("Paso #3: Ingresando datos para el cambio de contraseña...");

                    oldPassField.Clear();
                    oldPassField.SendKeys(oldPass);
                    Thread.Sleep(2000);
                    newPassField.Clear();
                    newPassField.SendKeys(newPass);
                    Thread.Sleep(2000);
                    confirmPassField.Clear();
                    confirmPassField.SendKeys(confirmPass);
                    Thread.Sleep(2000);

                    // Verificar que las contraseñas son iguales en los dos intentos
                    if (Utilities.Helpers.ElementExistsByPath(driver, "/html/body/ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-change-password/div[@class='modal-profile']//form[@role='form']//small[.=' Las contraseñas deben ser iguales ']"))
                    {
                        Debug.WriteLine("Resultado #3: Las contraseñas nuevas ingresadas no coinciden.");
                    }
                    else
                    {
                        Debug.WriteLine("Resultado #3: Los datos de la contraseña nueva coinciden.");
                        var buttonChangePassword = Utilities.Helpers.GetByXPathWithDelay(driver, "/html/body/ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-change-password//form[@role='form']//am-button[@title='Aceptar']/button[@type='submit']", 10);
                        Debug.WriteLine("Paso #4: Confirmando el cambio de contraseña con el botón de Aceptar...");
                        if (buttonChangePassword == null)
                        {
                            Debug.WriteLine("Resultado #4: Falla, no se encuentra el botón de cambio de contraseña.");
                        }
                        else
                        {
                            buttonChangePassword.Click();
                            // Confirmar que el mensaje de cambio de contraseña aparece
                            Thread.Sleep(4000);
                            if (Utilities.Helpers.ElementExistsByPath(driver, "//ngb-modal-window[@role='dialog']/div[@role='document']//am-success-modal//img[@class='img-fluid mb-4']"))
                            {
                                Debug.WriteLine("Resultado #4: Cambio de contraseña exitoso.");
                            }
                            else
                            {
                                Debug.WriteLine("Resultado #4: Falla el proceso de cambio de contraseña.");
                            }

                            Thread.Sleep(5000);
                        }
                        
                    }
                }
            }
        }

        [TestMethod]
        public void ChangePasswordOkTest()
        {

            string email = "m.vindasrod@gmail.com";
            string oldPass = "autoTest3";
            string newPass = "autoTest2";
            string confirmPass = "autoTest2";

            ChangePassword(email, oldPass, newPass, confirmPass);
        }

        [TestMethod]
        public void ChangePasswordNoMatchTest()
        {

            string email = "m.vindasrod@gmail.com";
            string oldPass = "autoTest3";
            string newPass = "autoTest2";
            string confirmPass = "autoTest3";

            ChangePassword(email, oldPass, newPass, confirmPass);
        }

        [TestCleanup]
        public void Fin()
        {
            driver.Quit();
        }
    }
}
