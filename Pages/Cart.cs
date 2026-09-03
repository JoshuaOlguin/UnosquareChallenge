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

        public IWebElement ProceedToCheckoutButton => wait.Until(d => driver.FindElement(By.Id("proceed-to-checkout-action")));
        public IWebElement CartSubtotal => wait.Until(d => driver.FindElement(By.Id("sc-subtotal-amount-buybox")));
        public IList<IWebElement> LinkButtons => wait.Until(d => driver.FindElements(By.CssSelector("input.a-color-link")));

        public IWebElement EmptyCartMessage => wait.Until(d => driver.FindElement(By.CssSelector("[class*='sc-list-item-removed-msg-delete a-padding-medium']")));

        public bool VerifyEmptyCartOperation()
        {
            try
            {
                return EmptyCartMessage.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void ClickOnDeleteItemLink()
        {
            var deleteLink = LinkButtons.FirstOrDefault();
            if (deleteLink == null)
                throw new InvalidOperationException("No delete link found in cart to click.");

            deleteLink.Click();
        }

        public void ClickOnProceedToCheckoutButton()
        {
            ProceedToCheckoutButton.Click();
        }

        public bool ProceedToCheckoutButtonIsVisibleAndClickable()
        {
            return ProceedToCheckoutButton != null && ProceedToCheckoutButton.Displayed && ProceedToCheckoutButton.Enabled;
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
