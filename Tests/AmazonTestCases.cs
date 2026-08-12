using AutomatedScript.Framework;
using AutomatedScript.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using Assert = NUnit.Framework.Assert;

namespace AutomatedScript.Tests
{
    [TestFixture]
    public class AmazonTestCases : TestInitialize
    {

        /// <summary>
        /// End-to-end UI test validating search, product detail, and cart add/remove operations
        /// for a targeted item ("Xbox Series X 1TB").
        /// </summary>
        /// <remarks>
        /// Test flow:
        /// 1. Navigate to Amazon home page.
        /// 2. Search for the product specified by <c>itemName</c>.
        /// 3. Verify the browser title contains the search term.
        /// 4. Capture the price of the first item shown in search results.
        /// 5. Open the product detail page for that item and capture the detail price.
        /// 6. Assert the search result price and detail page price are equal.
        /// 7. If the product was added to cart (verified via <c>VerifyCartCounter</c>):
        ///    a. Open the cart and capture the cart subtotal.
        ///    b. Assert the cart subtotal equals the previously captured price.
        ///    c. Remove the item from the cart and verify the cart is empty.
        /// 8. If the cart counter was not incremented, the test fails.
        ///
        /// Preconditions:
        /// - A valid WebDriver instance is available via the base class <c>TestInitialize</c>.
        /// - The test requires network access to https://www.amazon.com/.
        /// - Page object classes <c>Home</c>, <c>SearchFor</c>, <c>DetailProduct</c>, and <c>Cart</c> encapsulate UI interactions.
        ///
        /// Assertions and failure conditions:
        /// - Title must contain the search term.
        /// - Prices must match across search, detail and cart contexts.
        /// - Cart must be emptied successfully after deletion.
        /// </remarks>
        [Test]
        public void FirstTestCase()
        {
            string itemName = "Xbox Series X 1TB";

            Driver.Navigate().GoToUrl("https://www.amazon.com/");
            Home homeWebPage = new Home(Driver, Wait);
            homeWebPage.ContinueShopping();
            homeWebPage.SearchFor(itemName);

            bool titleContainsItem = Wait.Until(d => d.Title.Contains(itemName));
            Assert.IsTrue(titleContainsItem, $"Title : {Driver.Title} does not contain search parameter: {itemName}");

            SearchFor searchForWebPage = new SearchFor(Driver, Wait);
            var selectedProductPrice = searchForWebPage.GetPriceOfFirstItemOfSearchResult();
            searchForWebPage.ClickOnSelectedItem(searchForWebPage.SelectedItem);

            DetailProduct DetailProductWebPage = new DetailProduct(Driver, Wait);
            var detailProductPrice = DetailProductWebPage.GetPriceOfProduct();
            DetailProductWebPage.ClickOnFirstItemOfSearchResult();

            Assert.AreEqual(selectedProductPrice, detailProductPrice, "Selected product price : " + selectedProductPrice + " does not match detail price: " + detailProductPrice);

            if (DetailProductWebPage.VerifyCartCounter() == true)
            {
                DetailProductWebPage.ClickOnCartIcon();
                Cart CartWebPage = new Cart(Driver, Wait);
                var cartDetailPriceOfProduct = CartWebPage.GetCartSubtotal();

                Assert.AreEqual(selectedProductPrice, cartDetailPriceOfProduct, "Selected product price : " + selectedProductPrice + " does not match cart subtotal: " + cartDetailPriceOfProduct);
                CartWebPage.ClickOnDeleteItemLink();

                if (CartWebPage.VerifyEmptyCartOperation() == false)
                {
                    Assert.Fail("There was an error when automation script tried to empty shopping cart");
                }
            }
            else 
            {
                Assert.Fail("Product is not added to shopping cart.");
            }
        }

        /// <summary>
        /// End-to-end UI test that validates search, product detail, cart add/remove operations for a targeted item.
        /// </summary>
        /// <remarks>
        /// Test flow:
        /// 1. Navigate to Amazon home page.
        /// 2. Search for the product specified by <c>itemName</c>.
        /// 3. Verify the browser title contains the search term.
        /// 4. Capture the price of the first item shown in search results.
        /// 5. Open the product detail page for that item and capture the detail price.
        /// 6. Assert the search result price and detail page price are equal.
        /// 7. If the product was added to cart (verified via <c>VerifyCartCounter</c>):
        ///    a. Open the cart and capture the cart subtotal.
        ///    b. Assert the cart subtotal equals the previously captured price.
        ///    c. Remove the item from the cart and verify the cart is empty.
        /// 8. If the cart counter was not incremented, the test fails.
        ///
        /// Preconditions:
        /// - A valid WebDriver instance is available via the base class <c>TestInitialize</c>.
        /// - The test requires network access to https://www.amazon.com/.
        /// - Page object classes <c>Home</c>, <c>SearchFor</c>, <c>DetailProduct</c>, and <c>Cart</c> encapsulate UI interactions.
        ///
        /// Assertions and failure conditions produce meaningful messages to ease debugging:
        /// - Title must contain the search term.
        /// - Prices must match across search, detail and cart contexts.
        /// - Cart must be emptied successfully after deletion.
        /// </remarks>
        [Test]
        public void SecondTestCase()
        {
            string itemName = "PlayStation 5 Disc Edition Console";

            Driver.Navigate().GoToUrl("https://www.amazon.com/");
            Home homeWebPage = new Home(Driver, Wait);
            homeWebPage.ContinueShopping();
            homeWebPage.SearchFor(itemName);

            bool titleContainsItem = Wait.Until(d => d.Title.Contains(itemName));
            Assert.IsTrue(titleContainsItem, $"Title : {Driver.Title} does not contain search parameter: {itemName}");

            SearchFor searchForWebPage = new SearchFor(Driver, Wait);
            var selectedProductPrice = searchForWebPage.GetPriceOfFirstItemOfSearchResult();
            searchForWebPage.ClickOnSelectedItem(searchForWebPage.SelectedItem);

            DetailProduct DetailProductWebPage = new DetailProduct(Driver, Wait);
            var detailProductPrice = DetailProductWebPage.GetPriceOfProduct();
            DetailProductWebPage.ClickOnFirstItemOfSearchResult();

            Assert.AreEqual(selectedProductPrice, detailProductPrice, "Selected product price : " + selectedProductPrice + " does not match detail price: " + detailProductPrice);

            if (DetailProductWebPage.VerifyCartCounter() == true)
            {
                DetailProductWebPage.ClickOnCartIcon();
                Cart CartWebPage = new Cart(Driver, Wait);
                var cartDetailPriceOfProduct = CartWebPage.GetCartSubtotal();

                Assert.AreEqual(selectedProductPrice, cartDetailPriceOfProduct, "Selected product price : " + selectedProductPrice + " does not match cart subtotal: " + cartDetailPriceOfProduct);
                CartWebPage.ClickOnDeleteItemLink();

                if (CartWebPage.VerifyEmptyCartOperation() == false)
                {
                    Assert.Fail("There was an error when automation script tried to empty shopping cart");
                }
            }
            else
            {
                Assert.Fail("Product is not added to shopping cart.");
            }
        }

    }
}
