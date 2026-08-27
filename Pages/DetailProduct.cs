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

        public IWebElement AddToCartButton => wait.Until(d => driver.FindElement(By.CssSelector("input#add-to-cart-button.a-button-input")));
        public IWebElement PriceOfProduct => wait.Until(d => driver.FindElement(By.CssSelector("[class*='apex-pricetopay-value']")));
        public IWebElement CartIconButton => wait.Until(d => driver.FindElement(By.CssSelector("[class*='nav-a nav-a-2 nav-progressive-attribute']")));
        public IWebElement CartItemsCount;
        public bool VerifyCartCounter()
        {
            Console.WriteLine( $"Web page URL: { driver.Url.ToString() }");

            CartItemsCount = wait.Until(d => driver.FindElement(By.Id("nav-cart-count")));

            string CartItemsValueInnerText = CartItemsCount.GetAttribute("innerText"); ;
            Console.WriteLine("Cart items count: " + CartItemsValueInnerText);

            return CartItemsCount != null && !string.IsNullOrEmpty(CartItemsCount.Text) && Convert.ToInt32(CartItemsCount.Text) > 0;
        }

        public void AddToCartSelectedItem()
        {
            AddToCartButton.Click();

            var addToCartConfirmationMessage = wait.Until(d => driver.FindElement(By.Id("add-to-cart-confirmation-image")));

            if (addToCartConfirmationMessage.Displayed)
            {
                Console.WriteLine("Item added to cart successfully.");
            }
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
