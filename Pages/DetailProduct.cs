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

        public IWebElement AddToCartButton => wait.Until(d => driver.FindElement(By.Id("add-to-cart-button")).Displayed? driver.FindElement(By.Id("add-to-cart-button")) : null);
        public IWebElement PriceOfProduct => wait.Until(d => driver.FindElement(By.CssSelector("[class*='a-price aok-align-center apex-pricetopay-value']")).Displayed? driver.FindElement(By.CssSelector("[class*='a-price aok-align-center apex-pricetopay-value']")) : null);
        public IWebElement CartIconButton => wait.Until(d => driver.FindElement(By.CssSelector("[class*='nav-a nav-a-2 nav-progressive-attribute']")).Displayed ? driver.FindElement(By.CssSelector("[class*='nav-a nav-a-2 nav-progressive-attribute']")) : null);
        public IWebElement CartItemsCount => wait.Until(d => driver.FindElement(By.Id("nav-cart-count-container")).Displayed ? driver.FindElement(By.Id("nav-cart-count-container")) : null);
        public bool VerifyCartCounter()
        {
            try
            {
                wait.Until(d =>
                {
                    try
                    {
                        var el = d.FindElement(By.Id("nav-cart-count-container"));
                        return el != null && el.Text != null && el.Text.Contains("1");
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                });
                return true;
            }
            catch (WebDriverTimeoutException)
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
            string strPrice = PriceOfProduct.Text.Replace("MXN", string.Empty);
            strPrice = strPrice.Replace("\r\n", ".");
            decimal price;
            bool convt = decimal.TryParse(strPrice, out price);

            return convt == true ? price : 0;
        }
    }
}
