using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace AutomatedScript.Pages
{
    public class DetailProduct
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public DetailProduct(IWebDriver Driver, WebDriverWait Wait)
        {
            driver = Driver;
            wait = Wait;
        }

        public IWebElement AddToCartButton; 
        public IWebElement PriceOfProduct => wait.Until(d => driver.FindElement(By.CssSelector("[class*='apex-pricetopay-value']")));
        public IWebElement CartIconButton => wait.Until(d => driver.FindElement(By.CssSelector("[class*='nav-a nav-a-2 nav-progressive-attribute']")));
        public IWebElement CartItemsCount;
        public bool VerifyCartCounter()
        {
            CartItemsCount = wait.Until(d => driver.FindElement(By.Id("nav-cart-count")));
            return CartItemsCount != null && !string.IsNullOrEmpty(CartItemsCount.Text) && Convert.ToInt32(CartItemsCount.Text) > 0;
        }

        public void AddToCartSelectedItem()
        {
            AddToCartButton = wait.Until(d => driver.FindElement(By.CssSelector("input#add-to-cart-button.a-button-input")));
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
