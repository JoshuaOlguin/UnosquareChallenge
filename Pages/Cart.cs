using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomatedScript.Pages
{
    public class Cart
    {
        private RemoteWebDriver driver;
        private WebDriverWait wait;

        public Cart(RemoteWebDriver Driver, WebDriverWait Wait)
        {
            driver = Driver;
            wait = Wait;
        }

        public IWebElement ProceedToCheckoutButton => driver.FindElementById("proceed-to-checkout-action");
        public IWebElement PriceOfProduct => driver.FindElementByCssSelector("[class*='a-size-medium a-color-base sc-price sc-white-space-nowrap sc-product-price a-text-bold']");
        public IList<IWebElement> LinkButtons => driver.FindElements(By.CssSelector("input.a-color-link"));
        public IWebElement EmptyCartMessage => driver.FindElementByCssSelector("[class*='a-row sc-your-amazon-cart-is-empty']");

        public bool VerifyEmptyCartOperation()
        {
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[class*='a-row sc-your-amazon-cart-is-empty']")));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void ClickOnDeleteItemLink()
        {
            var deleteLink = LinkButtons.FirstOrDefault();
            deleteLink.Click();
        }

        public void ClickOnProceedToCheckoutButton()
        {
            ProceedToCheckoutButton.Click();
        }

        public bool ProceedToCheckoutButtonIsVisibleAndClickable()
        {
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(ProceedToCheckoutButton));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public decimal GetPriceOfProduct()
        {
            var strPrice = PriceOfProduct.Text;
            strPrice = String.Concat(strPrice.Where(x => x == '.' || Char.IsDigit(x)));
            decimal price;
            bool convt = decimal.TryParse(strPrice, out price);

            return convt == true ? price : 0;
        }
    }
}
