using NUnit.Framework;
using AutomatedScript.Framework;
using AutomatedScript.Pages;
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
            string itemName = "Samsung Galaxy Note 20";

            Driver.Navigate().GoToUrl("https://www.amazon.com/");
            Home homeWebPage = new Home(Driver, Wait);
            homeWebPage.SearchFor(itemName);
            Assert.IsTrue(Driver.Title.Contains(itemName));

            SearchFor searchForWebPage = new SearchFor(Driver, Wait);
            var selectedProductPrice = searchForWebPage.GetPriceOfFirstItemOfSearchResult();
            searchForWebPage.ClickOnSelectedItem(searchForWebPage.SelectedItem);

            DetailProduct DetailProductWebPage = new DetailProduct(Driver, Wait);
            var detailProductPrice = DetailProductWebPage.GetPriceOfProduct();
            DetailProductWebPage.ClickOnFirstItemOfSearchResult();

            Assert.AreEqual(selectedProductPrice, detailProductPrice);

            if (DetailProductWebPage.VerifyCartCounter() == true)
            {
                DetailProductWebPage.ClickOnCartIcon();
                Cart CartWebPage = new Cart(Driver, Wait);
                var cartDetailPriceOfProduct = CartWebPage.GetCartSubtotal();

                Assert.AreEqual(selectedProductPrice, cartDetailPriceOfProduct);
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
            string itemName = "Samsung Galaxy S20 FE 5G";

            Driver.Navigate().GoToUrl("https://www.amazon.com/");
            Home homeWebPage = new Home(Driver, Wait);
            homeWebPage.SearchFor(itemName);
            Assert.IsTrue(Driver.Title.Contains(itemName));

            SearchFor searchForWebPage = new SearchFor(Driver, Wait);
            var selectedProductPrice = searchForWebPage.GetPriceOfFirstItemOfSearchResult();
            searchForWebPage.ClickOnSelectedItem(searchForWebPage.SelectedItem);

            DetailProduct DetailProductWebPage = new DetailProduct(Driver, Wait);
            var detailProductPrice = DetailProductWebPage.GetPriceOfProduct();
            DetailProductWebPage.ClickOnFirstItemOfSearchResult();

            Assert.AreEqual(selectedProductPrice, detailProductPrice);

            if (DetailProductWebPage.VerifyCartCounter() == true)
            {
                DetailProductWebPage.ClickOnCartIcon();
                Cart CartWebPage = new Cart(Driver, Wait);
                var cartDetailPriceOfProduct = CartWebPage.GetCartSubtotal();

                Assert.AreEqual(selectedProductPrice, cartDetailPriceOfProduct);
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
