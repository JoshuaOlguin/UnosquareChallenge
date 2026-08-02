using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace AutomatedScript.Pages
{
    public class Home
    {
        private RemoteWebDriver driver;
        private WebDriverWait wait;

        public Home(RemoteWebDriver Driver, WebDriverWait Wait)
        {
            driver = Driver;
            wait = Wait;
        }

        public IWebElement SearchButton => wait.Until(d => driver.FindElement(By.Id("nav-search-submit-button")).Displayed? driver.FindElement(By.Id("nav-search-submit-button")) : null);
        public IWebElement SearchTextBox => wait.Until(d => driver.FindElement(By.Id("twotabsearchtextbox")).Displayed? driver.FindElement(By.Id("twotabsearchtextbox")) : null);
        
        public void SearchFor(string inputParameter)
        {
            SearchTextBox.SendKeys(inputParameter);
            SearchButton.Click();
        }

        public void MouseOverOnAccountOption()
        {
            Actions a = new Actions(driver);
            a.MoveToElement(driver.FindElement(By.CssSelector("[class*='nav-a nav-a-2   nav-progressive-attribute']"))).Build().Perform();
        }
    }
}
