using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
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

        public IList<IWebElement> AvailableItems => wait.Until(d =>
        {
            var elements = driver.FindElements(By.XPath("//div[contains(@class,'s-result-item') and contains(@class,'s-asin') and contains(@class,'sg-col-0-of-12') and contains(@class,'sg-col-16-of-20') and .//span[contains(@class,'a-price-whole')] and .//span[contains(@class,'a-price-fraction')]]"));
            return elements.Count > 0 ? elements : null;
        });

        public IWebElement SelectedItem;

        public void ClickOnSelectedItem(IWebElement item)
        {
            item.FindElement(By.CssSelector("[class*='a-size-medium a-spacing-none a-color-base a-text-normal']")).Click();
        }

        public decimal GetPriceOfFirstItemOfSearchResult()
        {
            SelectedItem = GetFirstOrDefaultItemWithPrice(AvailableItems);
            return  SetPriceOfFirstItemOfSearchResult(SelectedItem);
        }

        private IWebElement GetFirstOrDefaultItemWithPrice(IList<IWebElement> resultItems)
        {
            foreach (var item in resultItems)
            {
                if (item == null) continue;
                try
                {
                    if (item.FindElement(By.ClassName("a-price-whole")) != null && item.FindElement(By.ClassName("a-price-fraction")) != null) return item;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            }
            return null;
        }

        private decimal SetPriceOfFirstItemOfSearchResult(IWebElement selectedItem)
        {
            string strPrice = selectedItem.FindElement(By.ClassName("a-price-whole")).Text + "." + selectedItem.FindElement(By.ClassName("a-price-fraction")).Text;
            decimal price;
            bool convt = decimal.TryParse(strPrice, out price);
            return convt == true ? price : 0;
        }
    }
}
