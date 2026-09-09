using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomatedScript.Pages
{
    public class SearchFor
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public SearchFor(IWebDriver Driver, WebDriverWait Wait)
        {
            driver = Driver;
            wait = Wait;
        }

        private IList<IWebElement> AvailableItems => wait.Until(d =>
        {
            var elements = driver.FindElements(By.XPath("//div[contains(@class,'s-result-item') and contains(@class,'s-asin') and contains(@class,'sg-col-0-of-12') and contains(@class,'sg-col-16-of-20') and .//span[contains(@class,'a-price-whole')] and .//span[contains(@class,'a-price-fraction')]]"));
            return elements.Count > 0 ? elements : null;
        });

        private IWebElement SelectedItem;

        public void ClickOnSelectedItem(IWebElement item)
        {
            item.FindElement(By.CssSelector("[class*='a-size-medium a-spacing-none a-color-base a-text-normal']")).Click();
        }

        public IWebElement SelectFirstAvailableItemOfSearchResult()
        {
            SelectedItem = GetFirstOrDefaultItemWithPrice(AvailableItems);
            return SelectedItem;
        }

        public decimal GetPriceOfFirstItemOfSearchResult()
        {
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
