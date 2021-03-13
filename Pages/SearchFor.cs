using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace AutomatedScript.Pages
{
    public class SearchFor
    {
        private RemoteWebDriver driver;
        private WebDriverWait wait;

        public SearchFor(RemoteWebDriver Driver, WebDriverWait Wait)
        {
            driver = Driver;
            wait = Wait;
        }

        public IList<IWebElement> ResultItems => driver.FindElements(By.CssSelector("[class*='s-result-item s-asin sg-col-0-of-12 sg-col-16-of-20']"));

        public void ClickOnFirstItemOfSearchResult()
        {
            var selectedItem = ResultItems.FirstOrDefault();
            selectedItem.FindElement(By.CssSelector("[class*='a-size-medium a-color-base a-text-normal']")).Click();
        }

        public decimal GetPriceOfFirstItemOfSearchResult()
        {
            var selectedItem = ResultItems.FirstOrDefault();
            var strPrice = selectedItem.FindElement(By.ClassName("a-price-whole")).Text + "." + selectedItem.FindElement(By.ClassName("a-price-fraction")).Text;
            decimal price;
            bool convt = decimal.TryParse(strPrice, out price);
            
            return convt == true ? price : 0;
        }
    }
}
