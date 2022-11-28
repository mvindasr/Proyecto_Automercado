using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Proyecto_Automercado.Utilities;

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
            driver.Navigate().GoToUrl("https://automercado.cr");
            driver.Manage().Window.Maximize();
        }

        [TestMethod]
        public void AddContacts()
        {
            if (Helpers.Login(driver, "jazroxsal@gmail.com", "Matías2901"))
            {
                //PASO 1 ->  Abrir modal del método de compra.
                ChoosePurchaseMethod();

                Thread.Sleep(10000);

                // PASO 2 Abrir el formulario de contacto
                OpenContactForm();

                //PASO 3 llenar los espacios de texto del formulario
                FillForm();
                Thread.Sleep(10000);

                //PASO 4 Seleccionar ubicación en mapa
                SelectAddress();
                Thread.Sleep(10000);

                //PASO 5 Click en el botón de GUARDAR
                SaveContact();
            }
            else
            {
                Debug.WriteLine("Error al iniciar sesión.");
            }
            Thread.Sleep(10000);
        }

        public void ChoosePurchaseMethod()
        {
            Debug.WriteLine("Paso #1: Presionar el botón que dice \"elegir método de compra\"");
            driver.FindElement(By.XPath("/html/body/am-main//am-navbar[@class='ng-star-inserted']/nav//ul[@class='navbar-nav row w-100']//li[@class='nav-item p-0']/div/button")).Click();

            Debug.WriteLine("Resultado #1: Se muestra la ventana emergente para elegir el método de compra");
        }

        public void OpenContactForm()
        {
            Debug.WriteLine("Paso #2: Abrir el desplegable ¿Para quién es la compra?  y  seleccionar la opción agregar contacto.");

            driver.FindElement(By.XPath("/html//button[@id='dropdownForm1']")).Click();

            driver.FindElement(By.XPath("/html//ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-select-method/div/div[2]/div[1]/div[1]/div/button[1]")).Click();

            Debug.WriteLine("Resultado #2: Se muestra el formulario la agregar contacto.");
        }

        public void FillForm()
        {
            Debug.WriteLine("Paso #3: Rellenar los datos del formulario que se requieren escribir");

            var completeName = driver.FindElement(By.Id("thirdName"));
            completeName.SendKeys("Juan Carlos Hidalgo");

            var relationship = driver.FindElement(By.Id("thirdRelation"));
            relationship.SendKeys("Amigo");

            var phoneNumber = driver.FindElement(By.Id("thirdPhone"));
            phoneNumber.SendKeys("74125638");

            var observations = driver.FindElement(By.Id("observations"));
            observations.SendKeys("Apartamento 2015");

            Debug.WriteLine("Resultado #3: Permite ingresar los siguientes datos: Juan Carlos Hidalgo,  Amigo, 74125638, apartamento 2015");
        }

        public void SelectAddress()
        {
            Debug.WriteLine("Paso #4: Hacer click en el mapa del formulario para ingresar la dirección.");
            driver.FindElement(By.XPath("/html//ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-add-contact//form[@role='form']/div[4]")).Click();
            Thread.Sleep(10000);
            Debug.WriteLine("Resultado #4: Se visualiza el mapa para ingresar y seleccionar la dirección.");


            
            Debug.WriteLine("Paso #5: Escribir la dirección y seleccionar la que opción correcta");
            var address = driver.FindElement(By.Id("searchTextFieldDelivery"));
            address.SendKeys("iFreses, 50mts Norte y 50 Oeste, Freses, San José Province, Curridabat, Costa Rica");
            Thread.Sleep(10000);
            address.SendKeys(Keys.ArrowDown);
            address.SendKeys(Keys.Enter);
            Thread.Sleep(10000);
            Debug.WriteLine("Resultado #5:  En el mapa se visualiza el punto exacto de la dirección ingresada.");


            Debug.WriteLine("Resultado #6: Presionar botón de continuar");
            driver.FindElement(By.XPath("/html//ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-select-address-map//am-button/button[@type='button']")).Click();
            Debug.WriteLine("Resultado #6:  Nos devuelve a la vista del formulario de agregar contacto.");
        }

        public void SaveContact()
        {
            Debug.WriteLine("Paso #7: Presionar el botón que dice \"Guardar\"");
            driver.FindElement(By.XPath("/html//ngb-modal-window[@role='dialog']/div[@role='document']//am-modal-add-contact//form[@role='form']//am-button/button[@type='submit']")).Click();
            Debug.WriteLine("Resultado #7:  Guarda el contacto y muestra en pantalla un mensaje que dice \"Contacto creado con éxito\".");
        }




        [TestCleanup]
        public void Fin()
        {
            driver.Quit();
        }
    }
}
