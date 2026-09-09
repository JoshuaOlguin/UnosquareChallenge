using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomatedScript.Pages
{
    public class DetailProduct
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public DetailProduct(IWebDriver Driver, WebDriverWait Wait)
        {
            driver = Driver;
            wait = Wait;
        }

        private IWebElement AddToCartButton => wait.Until(d => driver.FindElement(By.CssSelector("input#add-to-cart-button.a-button-input")));
        private IWebElement PriceOfProduct => wait.Until(d => driver.FindElement(By.CssSelector("[class*='apex-pricetopay-value']")));
        private IWebElement CartIconButton => wait.Until(d => driver.FindElement(By.CssSelector("[class*='nav-a nav-a-2 nav-progressive-attribute']")));
        private IWebElement GoToCartButton => wait.Until(d => driver.FindElement(By.CssSelector("span.a-button.a-button-span11.a-button-base.a-button-small span.a-button-inner a.a-button-text")));
        private IWebElement RefuseCoverageForAccidentalDamageButton => wait.Until(d => driver.FindElement(By.XPath("//input[@class='a-button-input' and @aria-labelledby='attachSiNoCoverage-announce']")));

        public void AddToCartSelectedItem()
        {
            AddToCartButton.Click();
        }

        public bool VerifyCartCounter()
        {
            IWebElement CartItemsCount = wait.Until(d => driver.FindElement(By.XPath("//span[@id='nav-cart-count']")));
            return CartItemsCount != null && !string.IsNullOrEmpty(CartItemsCount.Text) && Convert.ToInt32(CartItemsCount.Text) > 0;
        }

        public void RefuseCoverageForAccidentalDamageProduct()
        {
            try 
            {
                if (RefuseCoverageForAccidentalDamageButton.Displayed)
                {
                    RefuseCoverageForAccidentalDamageButton.Click();
                }
            }
            catch { }
        }

        public void ClickOnGoToCartButton()
        {
            GoToCartButton.Click();
        }

        public void ClickOnCartIcon()
        {
            CartIconButton.Click();
        }

        public decimal GetPriceOfProduct()
        {
            string strPrice = PriceOfProduct.FindElement(By.ClassName("a-price-whole")).Text + "." + PriceOfProduct.FindElement(By.ClassName("a-price-fraction")).Text;
            decimal price;
            bool convt = decimal.TryParse(strPrice, out price);
            return convt == true ? price : 0;
        }
    }
}
