using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace AutomatedScript.Pages
{
    public class DetailProduct
    {
        private RemoteWebDriver driver;
        private WebDriverWait wait;

        public DetailProduct(RemoteWebDriver Driver, WebDriverWait Wait)
        {
            driver = Driver;
            wait = Wait;
        }

        public IWebElement AddToCartButton => driver.FindElementById("add-to-cart-button");
        public IWebElement PriceOfProduct => driver.FindElementById("exports_desktop_qualifiedBuybox_priceInsideBuyBox");
        public IWebElement CartIconButton => driver.FindElementByCssSelector("[class*='nav-a nav-a-2 nav-progressive-attribute']");
        public IWebElement CartItemsCount => driver.FindElementById("nav-cart-count-container");


        public bool VerifyCartCounter()
        {
            try
            {
                wait.Until(ExpectedConditions.TextToBePresentInElement(CartItemsCount, "1"));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void ClickOnFirstItemOfSearchResult()
        {
            AddToCartButton.Click();
        }

        public void ClickOnCartIcon()
        {
            CartIconButton.Click();
        }

        public decimal GetPriceOfProduct()
        {
            var strPrice = PriceOfProduct.FindElement(By.CssSelector("[class*='a-size-medium a-color-price']")).Text;
            strPrice = String.Concat(strPrice.Where(x => x == '.' || Char.IsDigit(x)));
            decimal price;
            bool convt = decimal.TryParse(strPrice, out price);

            return convt == true ? price : 0;
        }
    }
}
