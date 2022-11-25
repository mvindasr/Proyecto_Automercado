using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Proyecto_Automercado.Utilities;
using System.Diagnostics;
using System.Security.Claims;

namespace Proyecto_Automercado
{
    [TestClass]
    public class PH03
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
        public void addFavoriteProduct()
        {

            // Se inicia sessión.
            InitSession();
            
            // Se selecciona un producto.
            string pIn = SelectProduct();

            // Se ve la lista de favoritos. ("Compras")
            string pOut = ViewFavoriteList();

            // Se compara para hacer la prueba final y saber si se agrego correctamente.
            CompareInOut(pIn, pOut);

        }

        private void InitSession()
        {

            // Constantes para contraseña y clave.
            string EMAIL = "tomatejuan@gmail.com";
            string PASS = "5Y5&QD6F9mzLHMm!";

            // Se inicia sesión.
            if (Helpers.Login(driver, EMAIL, PASS))
            {
           
            }
            else
            {
                Debug.WriteLine("Error al iniciar sesión.");
            }
            Thread.Sleep(3000);

            Debug.WriteLine("Paso #1: Inicia sessión.");
        }

        private string SelectProduct()
        {
            
            // Busca la categoria donde esta el producto.
            var category = Helpers.GetByXPathWithDelay(driver, "/html//am-main//am-home[@class='ng-star-inserted']/am-our-halls//section//div[@class='col-12']/div/div[1]/am-product-category//img[@alt='Abarrotes']", 10);
            category.Click();

            Thread.Sleep(3000);
            Debug.WriteLine("Paso #2: Se busca el producto.");


            // Revisa si existe el producto.
             Helpers.GetByXPathWithDelay(driver, "/html/body[@class='vsc-initialized']/am-main//am-product-search[@class='ng-star-inserted']/div[@class='list']/div[@class='container']/div/div[@class='col-12 col-lg-10']//div[@class='grid-square']/div[1]/am-product-list//div[@class='card card-product']//a[@title='VINAGRE BALSAMICO 3 HOJAS MAZZETTI botella 250 mL']", 10);
            
            // Seleciona el producto.
            var ProductSelected =  driver.FindElement(By.XPath("/html//am-main//am-product-search[@class='ng-star-inserted']/div[@class='list']/div[@class='container']/div/div[@class='col-12 col-lg-10']//div[@class='grid-square']/div[1]/am-product-list//div[@class='card card-product']//a[@title='VINAGRE BALSAMICO 3 HOJAS MAZZETTI botella 250 mL']/span[.='VINAGRE BALSAMICO 3 HOJAS MAZZETTI botella 250 mL']")).GetAttribute("innerText");

            Debug.WriteLine("Producto seleccionado: " + ProductSelected);
   
            // Marca el producto como favorito.
            MarkFavorite();

            return ProductSelected;


        }

       
        private void MarkFavorite()
        {

            Debug.WriteLine("Paso #3: Marca producto como favorito.");

            driver.FindElement(By.CssSelector("i[_ngcontent-automercado-c120]")).Click();

            Thread.Sleep(3000);

            Debug.WriteLine("Paso #4: Lo agrega a la lista.");
            // Click en la lista a la que se va a guardar.
            var BtnAddListSite = Helpers.GetByXPathWithDelay(driver, "/html/body/ngb-modal-window[@role='dialog']/div[@role='document']//am-save-list//div[@class='regular-text text-truncate']", 10);
            BtnAddListSite.Click();

            Thread.Sleep(3000);

            // Se acepta el guardado (click en el boton guardado)
            var BtnSaveFavorite = Helpers.GetByXPathWithDelay(driver, "//ngb-modal-window[@role='dialog']/div[@role='document']//am-save-list//am-button[@title='Guardar']/button[@type='button']", 10);
            BtnSaveFavorite.Click();

            // Se Cierra el modal
            var BtnModalSaveOut = Helpers.GetByXPathWithDelay(driver, "//ngb-modal-window[@role='dialog']/div[@role='document']//am-success-modal//button[@type='button']/span[.='×']", 10);
            BtnModalSaveOut.Click();

            Thread.Sleep(3000);
        }

        private string ViewFavoriteList()
        {
            Debug.WriteLine("Paso #5: Abrir la lista de favoritos.");

            // Vamos a la URL de la lista.
            driver.Navigate().GoToUrl("https://automercado.cr/perfil/mis-listas");

            Thread.Sleep(3000);

            // Cerramos el Modal del "Plan A"
            var BtnQuitModalONE = driver.FindElement(By.ClassName("introjs-skipbutton"));
            BtnQuitModalONE.Click();

            // Thread.Sleep(3000);

            // Cerramos el Modal del "Explora Sitio Web"
            // var BtnQuitModalTwo = driver.FindElement(By.ClassName("btn am-button"));
           // BtnQuitModalTwo.Click();

            Thread.Sleep(3000);

            var BtnView = Helpers.GetByXPathWithDelay(driver, "/html//am-main//am-profile-layout[@class='ng-star-inserted']/am-my-list[@class='ng-star-inserted']/div[@class='my-list']/div[2]/div//div[@class='col p-0']/div[1]/a[@href='javascript:;']", 10);
            BtnView.Click();

            // Seleciona el producto en la lista.
      
            Thread.Sleep(3000);
            var ProductInList = driver.FindElement(By.ClassName("title-1")).GetAttribute("innerText");

            Debug.WriteLine("Producto en la lista: " + ProductInList);

            return ProductInList;
        }

        private void CompareInOut(string inputProduct, string outputProduct)
        {
            Debug.WriteLine("Paso #6: Se comparan la entrada con la salida.");

            // Se comparan los datos de entrada y salida.
            bool result = inputProduct.Equals(outputProduct, StringComparison.OrdinalIgnoreCase);

            if (result == true) { 
                Debug.WriteLine("Resultado #1: El producto si se agrego correctamente a la lista de favoritos.");
                Debug.WriteLine("Producto agregado: " + outputProduct);
            } else {
                Debug.WriteLine("Resultado #2: El producto NO se agrego correctamente a la lista de favoritos.");
            }

        }

        [TestCleanup]
        public void Fin()
        {
            driver.Quit();
        }
    }
}
