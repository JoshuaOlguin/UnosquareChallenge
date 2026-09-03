using AutomatedScript.Framework;
using AutomatedScript.Pages;
using NUnit.Framework;

namespace AutomatedScript.Tests
{
    [TestFixture]
    public class AmazonTestCases : TestInitialize
    {
        
        /// <summary>
        /// Validates the end-to-end shopping workflow on Amazon, including search, product selection, price verification, cart operations, and item removal.
        /// </summary>
        /// <remarks>
        /// This test performs the following steps:
        /// 1. Navigates to Amazon homepage and searches for "Xbox Series X 1TB"
        /// 2. Verifies the search results page title contains the search term
        /// 3. Retrieves the price of the first search result
        /// 4. Navigates to the product detail page and verifies the price matches
        /// 5. Adds the product to the shopping cart
        /// 6. Verifies the cart counter is incremented
        /// 7. Validates the cart subtotal matches the product price
        /// 8. Removes the item from the cart
        /// 9. Confirms the shopping cart is empty
        /// </remarks>
        [Test]
        public void ValidateEndToEndShoppingWorkflow_XboxSeriesX()
        {
            string itemName = "Xbox Series X 1TB";

            Driver.Navigate().GoToUrl("https://www.amazon.com/");
            Home homeWebPage = new Home(Driver, Wait);
            homeWebPage.ContinueShopping();
            homeWebPage.SearchFor(itemName);

            bool titleContainsItem = Wait.Until(d => d.Title.Contains(itemName));
            Assert.IsTrue(titleContainsItem, $"Title : {Driver.Title} does not contain search parameter: {itemName}");

            SearchFor searchForWebPage = new SearchFor(Driver, Wait);
            searchForWebPage.SelectFirstAvailableItemOfSearchResult();
            var selectedProductPrice = searchForWebPage.GetPriceOfFirstItemOfSearchResult();
            searchForWebPage.ClickOnSelectedItem(searchForWebPage.SelectedItem);

            DetailProduct DetailProductWebPage = new DetailProduct(Driver, Wait);
            var detailProductPrice = DetailProductWebPage.GetPriceOfProduct();
            Assert.AreEqual(selectedProductPrice, detailProductPrice, "Selected product price : " + selectedProductPrice + " does not match detail price: " + detailProductPrice);

            DetailProductWebPage.AddToCartSelectedItem();
            DetailProductWebPage.RefuseCoverageForAccidentalDamageProduct();
            DetailProductWebPage.ClickOnGoToCartButton();
            Assert.IsTrue(DetailProductWebPage.VerifyCartCounter(), "Cart counter was not incremented.");

            Cart CartWebPage = new Cart(Driver, Wait);
            var cartDetailPriceOfProduct = CartWebPage.GetCartSubtotal();
            Assert.AreEqual(selectedProductPrice, cartDetailPriceOfProduct, "Selected product price : " + selectedProductPrice + " does not match cart subtotal: " + cartDetailPriceOfProduct);

            CartWebPage.ClickOnDeleteItemLink();
            Assert.IsTrue(CartWebPage.VerifyEmptyCartOperation(), "Failed to empty the shopping cart.");
        }

        /// <summary>
        /// Validates the end-to-end shopping workflow on Amazon, including search, product selection, price verification, cart operations, and item removal.
        /// </summary>
        /// <remarks>
        /// This test performs the following steps:
        /// 1. Navigates to Amazon homepage and searches for "Xbox Series X 1TB"
        /// 2. Verifies the search results page title contains the search term
        /// 3. Retrieves the price of the first search result
        /// 4. Navigates to the product detail page and verifies the price matches
        /// 5. Adds the product to the shopping cart
        /// 6. Verifies the cart counter is incremented
        /// 7. Validates the cart subtotal matches the product price
        /// 8. Removes the item from the cart
        /// 9. Confirms the shopping cart is empty
        /// </remarks>
        [Test]
     public void ValidateEndToEndShoppingWorkflow_PlayStation5()
     {
            string itemName = "PlayStation 5 Disc Edition Console";

            Driver.Navigate().GoToUrl("https://www.amazon.com/");
            Home homeWebPage = new Home(Driver, Wait);
            homeWebPage.ContinueShopping();
            homeWebPage.SearchFor(itemName);

            bool titleContainsItem = Wait.Until(d => d.Title.Contains(itemName));
            Assert.IsTrue(titleContainsItem, $"Title : {Driver.Title} does not contain search parameter: {itemName}");

            SearchFor searchForWebPage = new SearchFor(Driver, Wait);
            searchForWebPage.SelectFirstAvailableItemOfSearchResult();
            var selectedProductPrice = searchForWebPage.GetPriceOfFirstItemOfSearchResult();
            searchForWebPage.ClickOnSelectedItem(searchForWebPage.SelectedItem);

            DetailProduct DetailProductWebPage = new DetailProduct(Driver, Wait);
            var detailProductPrice = DetailProductWebPage.GetPriceOfProduct();
            Assert.AreEqual(selectedProductPrice, detailProductPrice, "Selected product price : " + selectedProductPrice + " does not match detail price: " + detailProductPrice);

            DetailProductWebPage.AddToCartSelectedItem();
            DetailProductWebPage.RefuseCoverageForAccidentalDamageProduct();
            DetailProductWebPage.ClickOnGoToCartButton();
            Assert.IsTrue(DetailProductWebPage.VerifyCartCounter(), "Cart counter was not incremented.");

            Cart CartWebPage = new Cart(Driver, Wait);
            var cartDetailPriceOfProduct = CartWebPage.GetCartSubtotal();
            Assert.AreEqual(selectedProductPrice, cartDetailPriceOfProduct, "Selected product price : " + selectedProductPrice + " does not match cart subtotal: " + cartDetailPriceOfProduct);

            CartWebPage.ClickOnDeleteItemLink();
            Assert.IsTrue(CartWebPage.VerifyEmptyCartOperation(), "Failed to empty the shopping cart.");
        }
    }
}
