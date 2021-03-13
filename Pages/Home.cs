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

        public IWebElement SearchButton => driver.FindElementById("nav-search-submit-button");

        public IWebElement SearchTextBox => driver.FindElementById("twotabsearchtextbox");

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
