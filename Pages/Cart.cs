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
        private IWebDriver driver;
        private WebDriverWait wait;

        public Cart(IWebDriver Driver, WebDriverWait Wait)
        {
            driver = Driver;
            wait = Wait;
        }
        
        public IWebElement ProceedToCheckoutButton => wait.Until(d => driver.FindElement(By.Id("proceed-to-checkout-action")).Displayed ? driver.FindElement(By.Id("proceed-to-checkout-action")) : null);
        public IWebElement CartSubtotal => wait.Until(d => driver.FindElement(By.Id("sc-subtotal-amount-buybox")).Displayed ? driver.FindElement(By.Id("sc-subtotal-amount-buybox")) : null);
        public IList<IWebElement> LinkButtons => wait.Until(d => driver.FindElements(By.CssSelector("input.a-color-link")).Count > 0 ? driver.FindElements(By.CssSelector("input.a-color-link")) : null);

        public IWebElement EmptyCartMessage;

        public bool VerifyEmptyCartOperation()
        {
            try
            {
                EmptyCartMessage = wait.Until(d => driver.FindElement(By.CssSelector("[class*='sc-list-item-removed-msg-delete a-padding-medium']")).Displayed ? driver.FindElement(By.CssSelector("[class*='sc-list-item-removed-msg-delete a-padding-medium']")) : null);
                return EmptyCartMessage != null ? EmptyCartMessage.Displayed : false;
            }
            catch (NoSuchElementException)
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
                wait.Until(d =>
                {
                    try
                    {
                        var el = d.FindElement(By.Id("proceed-to-checkout-action"));
                        return el != null && el.Displayed && el.Enabled;
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

        public decimal GetCartSubtotal()
        {
            var strPrice = CartSubtotal.Text;
            strPrice = String.Concat(strPrice.Where(x => x == '.' || Char.IsDigit(x)));
            decimal price;
            bool convt = decimal.TryParse(strPrice, out price);

            return convt == true ? price : 0;
        }
    }
}
