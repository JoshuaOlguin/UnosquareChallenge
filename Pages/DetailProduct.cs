using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.Extensions.Logging;

namespace AutomatedScript.Pages
{
    public class DetailProduct
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private readonly ILogger<DetailProduct> _logger;

        public DetailProduct(IWebDriver Driver, WebDriverWait Wait)
        {
            driver = Driver;
            wait = Wait;
            _logger = new LoggerFactory().CreateLogger<DetailProduct>();
        }

        public IWebElement AddToCartButton => wait.Until(d => driver.FindElement(By.CssSelector("input#add-to-cart-button.a-button-input")));
        public IWebElement PriceOfProduct => wait.Until(d => driver.FindElement(By.CssSelector("[class*='apex-pricetopay-value']")));
        public IWebElement CartIconButton => wait.Until(d => driver.FindElement(By.CssSelector("[class*='nav-a nav-a-2 nav-progressive-attribute']")));
        public IWebElement CartItemsCount;
        public bool VerifyCartCounter()
        {
            try 
            {
                CartItemsCount = wait.Until(d => driver.FindElement(By.XPath("//span[@id='nav-cart-count']")));
                return CartItemsCount != null && CartItemsCount.Text != null && Convert.ToInt32(CartItemsCount.Text) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while publishing result. Exception: {ExceptionMessage}", ex.Message + CartItemsCount.Text);
                return false;
            }
            
        }

        public void AddToCartSelectedItem()
        {
            AddToCartButton.Click();
        }

        public void ClickOnCartIcon()
        {
            CartIconButton.Click();
        }

        public decimal GetPriceOfProduct()
        {
            string strPrice = PriceOfProduct.FindElement(By.ClassName("a-price-whole")).Text + "." + PriceOfProduct.FindElement(By.ClassName("a-price-fraction")).Text;
            decimal price;
            bool convt = decimal.TryParse(strPrice, out price);
            return convt == true ? price : 0;
        }
    }
}
