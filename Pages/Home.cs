using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace AutomatedScript.Pages
{
    public class Home
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public Home(IWebDriver Driver, WebDriverWait Wait)
        {
            driver = Driver;
            wait = Wait;
        }

        public IWebElement SearchButton => wait.Until(d => driver.FindElement(By.Id("nav-search-submit-button")));
        public IWebElement SearchTextBox => wait.Until(d => driver.FindElement(By.Id("twotabsearchtextbox")));
        public IWebElement ContinueShoppingButton => wait.Until(d => driver.FindElement(By.XPath("//button[@class='a-button-text' and text()='Continue shopping']")));

        public void SearchFor(string inputParameter)
        {
            SearchTextBox.SendKeys(inputParameter);
            SearchButton.Click();
        }

        public void ContinueShopping() 
        {
            try
            {
                if (ContinueShoppingButton.Displayed)
                {
                    ContinueShoppingButton.Click();
                }
            }
            catch {}
        }
    }
}
