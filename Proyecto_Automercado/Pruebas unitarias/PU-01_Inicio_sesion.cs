using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;

namespace Proyecto_Automercado
{
    [TestClass]
    public class PU01
    {
        IWebDriver driver;
        [TestInitialize]
        public void Start()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://automercado.cr/");
            driver.Manage().Window.Maximize();
        }

        public void LoginProcess(string setEmail, string setPassword, string user)
        {
            //Obtengo el bot�n de Login y lo presiono
            var loginButton = driver.FindElement(By.Id("login"));
            Debug.WriteLine("Paso #1: Presionando bot�n de inicio de sesi�n...");
            loginButton.Click();

            //En el modal, busco el campo para el correo, lo limpio y env�o mi valor
            Debug.WriteLine("Resultado #1: Se abre el modal para ingresar las credenciales de usuario.");
            var inputEmail = driver.FindElement(By.Id("email"));
            inputEmail.Clear();
            inputEmail.SendKeys(setEmail);


            //En el modal, busco el campo para la contrase�a, lo limpio y env�o mi valor
            var inputPassword = driver.FindElement(By.Id("password"));
            inputPassword.Clear();
            inputPassword.SendKeys(setPassword);
            Debug.WriteLine("Paso #2: Digitando como correo '" +  setEmail + "' y como contrase�a '" + setPassword + "'...");


            //Verifico que el valor de correo y contrase�a se enviaron correctamente
            string mailInserted = inputEmail.GetAttribute("value");
            string passwordInserted = inputPassword.GetAttribute("value");

            if (Utilities.Helpers.CompareData(mailInserted, setEmail) && Utilities.Helpers.CompareData(passwordInserted, setPassword))
            {
                Debug.WriteLine("Resultado #2: Los datos se ingresaron correctamente sin ning�n reporte de error en el modal.");

                //Encuentro el bot�n de inicio de sesi�n y lo presiono
                var loginCredentialsButton = Utilities.Helpers.GetByXPath(driver, "/html/body/ngb-modal-window[@role='dialog']/div[@role='document']//am-login-modal//form[@role='form']//am-button/button[@type='submit']");
                Debug.WriteLine("Paso #3: Se presiona el bot�n de inicio de sesi�n con las credenciales...");
                if (loginCredentialsButton != null)
                {
                    loginCredentialsButton.Click();

                    //Confirmo que inici� sesi�n al verificar mi nombre de perfil en la bienvenida
                    var username = Utilities.Helpers.GetByXPathWithDelay(driver, "//button[@id='dropdownManual']//b[.='�Hola Milton Vindas!']", 10);

                    if (username != null)
                    {
                        string loggedUsername = username.Text;

                        if (Utilities.Helpers.CompareData(loggedUsername, user))
                        {
                            Debug.WriteLine("Resultado #3: Inicio de sesi�n exitosa, se confirma el mensaje de bienvenida: '" + loggedUsername + "'.");
                        }
                        else
                        {
                            Debug.WriteLine("Resultado #3: Fallo en el inicio de sesi�n.");
                        }
                    }

                    else
                    {
                        Debug.WriteLine("Resultado #3: Falla. No se encontr� el elemento de inicio de sesi�n exitosa.");
                    }
                }
                else
                {
                    Debug.WriteLine("Resultado #3: Falla. No se encuentra el bot�n de inicio de sesi�n.");
                }
            }
            else
            {
                Debug.WriteLine("Resultado #2: Falla. Error al ingresar los datos.");
            }

        }

        [TestMethod]
        public void LoginOkCredentialsTest()
        {
            //Variables de prueba
            var setEmail = "mvindasr@ucenfotec.ac.cr";
            var setPassword = "autoTest1";
            var user = "�Hola Milton Vindas!";

            LoginProcess(setEmail, setPassword, user);
        }

        [TestMethod]
        public void LoginBadCredentialsTest()
        {
            //Variables de prueba
            var setEmail = "mvindasr@ucenfotec.ac.cr";
            var setPassword = "autoTest100";
            var user = "�Hola Milton Vindas!";

            LoginProcess(setEmail, setPassword, user);
        }

        [TestCleanup]
        public void Fin()
        {
            driver.Quit();
        }
    }
}
