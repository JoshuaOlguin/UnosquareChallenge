using System;
using NUnit.Framework;
using AutomatedScript.Framework;
using AutomatedScript.Pages;
using OpenQA.Selenium;
using AutomatedScript.Utilities;
using System.Linq;
using OpenQA.Selenium.Interactions;
using AutomatedScript.Services;

namespace AutomatedScript.Tests
{
    [TestFixture]
    public class AmazonTestCases : TestInitialize
    {
        /// <summary>
        /// First Test Case:
        /// 1.- Go to Amazon.com
        /// 2.- Search for Samsung Galaxy Note 20
        /// 3.- Verify Item is displayed on the screen and locate the first one, then store the price
        /// 4.- Click on the First Result
        /// 5.- Once in the details page compare this price vs the above one(first stored price)
        /// 6.- Click on Add to Cart
        /// 7.- Go to Cart and verify again the price of the phone
        /// 8.- Delete Item
        /// </summary>
        [Test]
        public void FirstTestCase()
        {
            Home HomeWebPage = new Home(Driver, Wait);
            HomeWebPage.SearchFor("Samsung Galaxy Note 20");
            Assert.IsTrue(Driver.Title.Contains("Samsung Galaxy Note 20"));

            SearchFor SearchForWebPage = new SearchFor(Driver, Wait);
            var selectedProductPrice = SearchForWebPage.GetPriceOfFirstItemOfSearchResult();
            SearchForWebPage.ClickOnFirstItemOfSearchResult();

            DetailProduct DetailProductWebPage = new DetailProduct(Driver, Wait);
            var detailProductPrice = DetailProductWebPage.GetPriceOfProduct();
            DetailProductWebPage.ClickOnFirstItemOfSearchResult();

            Assert.AreEqual(selectedProductPrice, detailProductPrice);

            if (DetailProductWebPage.VerifyCartCounter() == true)
            {
                DetailProductWebPage.ClickOnCartIcon();
                Cart CartWebPage = new Cart(Driver, Wait);
                var cartDetailPriceOfProduct = CartWebPage.GetPriceOfProduct();

                Assert.AreEqual(selectedProductPrice, cartDetailPriceOfProduct);
                CartWebPage.ClickOnDeleteItemLink();

                if (CartWebPage.VerifyEmptyCartOperation() == false)
                {
                    Assert.Fail("There was an error when system tried to empty cart");
                }
            }
            else 
            {
                Assert.Fail("Product is not added to cart.");
            }
        }

        /// <summary>
        /// Second Test Case:
        /// 1.- Go to Amazon.com
        /// 2.- Search for Samsung Galaxy S20 FE 5G
        /// 3.- Verify Item is displayed on the screen and locate the first one, then store the price
        /// 4.- Click on the First Result
        /// 5.- Once in the details page compare this price vs the above one
        /// 6.- Click on Add to Cart
        /// 7.- Go to Cart and verify again the price of the phone
        /// 8.- Delete Item
        /// </summary>
        [Test]
        public void SecondTestCase()
        {
            Home HomeWebPage = new Home(Driver, Wait);
            HomeWebPage.SearchFor("Samsung Galaxy S20 FE 5G");
            Assert.IsTrue(Driver.Title.Contains("Samsung Galaxy S20 FE 5G"));

            SearchFor SearchForWebPage = new SearchFor(Driver, Wait);
            var selectedProductPrice = SearchForWebPage.GetPriceOfFirstItemOfSearchResult();
            SearchForWebPage.ClickOnFirstItemOfSearchResult();

            DetailProduct DetailProductWebPage = new DetailProduct(Driver, Wait);
            var detailProductPrice = DetailProductWebPage.GetPriceOfProduct();
            DetailProductWebPage.ClickOnFirstItemOfSearchResult();

            Assert.AreEqual(selectedProductPrice, detailProductPrice);

            if (DetailProductWebPage.VerifyCartCounter() == true)
            {
                DetailProductWebPage.ClickOnCartIcon();
                Cart CartWebPage = new Cart(Driver, Wait);
                var cartDetailPriceOfProduct = CartWebPage.GetPriceOfProduct();

                Assert.AreEqual(selectedProductPrice, cartDetailPriceOfProduct);
                CartWebPage.ClickOnDeleteItemLink();

                if (CartWebPage.VerifyEmptyCartOperation() == false)
                {
                    Assert.Fail("There was an error when system tried to empty cart");
                }
            }
            else
            {
                Assert.Fail("Product is not added to cart.");
            }
        }

        /// <summary>
        /// Third Test Case:
        /// 1.- Go to Amazon main page
        /// 2,- Locate at the upper right corner the button: Hello, Sign In Account & lists and click on it
        /// 3.- Click on "New customer? Start right here"
        /// 4.- Fill Name field with the response of this API => [Options in the AC]
        /// 5.- Fill Email field with the data from the API response Firstname.Lastname @fake.com
        /// 6.- Click on Condition of Use link
        /// 7.- Locate the search bar and Search for Echo
        /// 8.- Locate "Echo support" options and click on it
        /// 9.- Following elements should be displayed: Getting Started, Wi-Fi and Bluetooth, Device Software and Hardware, TroubleShooting
        /// </summary>
        [Test]
        public void ThirdTestCase()
        {
            var temp = new Employee().GetEmployeeById(1);
            Home HomeWebPage = new Home(Driver, Wait);

            Actions a = new Actions(Driver);
            a.MoveToElement(Driver.FindElement(By.CssSelector("[class*='nav-a nav-a-2   nav-progressive-attribute']"))).Build().Perform();
        }
    }
}
