using Kata.ShoppingCart.Discounts;
using Kata.ShoppingCart.Exceptions;
using Kata.ShoppingCart.Repositories;
using Kata.ShoppingCart.Tests.NUnit.Stubs;
using NUnit.Framework;

namespace Kata.ShoppingCart.Tests.NUnit
{
    public class CheckoutTests
    {
        private readonly IProductRepository _productsRepo = new TestProductRepository();

        private readonly IDiscountRepository _discountRepo = new Stubs.TestDiscountRepository();

        [SetUp]
        public void Setup()
        {
            _productsRepo.Add(new ShoppingProduct('A', 50));
            _productsRepo.Add(new ShoppingProduct('B', 30));
            _productsRepo.Add(new ShoppingProduct('C', 20));
            _productsRepo.Add(new ShoppingProduct('D', 15));

            _discountRepo.Add(new QuantityDiscount('A', 3, 130));
            _discountRepo.Add(new QuantityDiscount('B', 2, 45));
        }

        [Test]
        [TestCase("DABA", 145)]
        [TestCase("BBBB", 90)]
        public void Checkout_GetTotal_Should_ReturnExpectedTotal(string products, double expectedPrice)
        {
            var checkout = new Checkout(_productsRepo, _discountRepo);

            var total = checkout.GetTotal(products);

            Assert.AreEqual(expectedPrice, total);
        }

        [Test]
        public void Checkout_GetTotal_Should_Return0_When_Products_Null()
        {
            var checkout = new Checkout(_productsRepo, _discountRepo);

            var total = checkout.GetTotal(null);

            Assert.AreEqual(0, total);
        }

        [Test]
        public void Checkout_GetTotal_Should_Fail_When_Unknown_Product_Provided()
        {
            var checkout = new Checkout(_productsRepo, _discountRepo);

            Assert.Catch<ProductMissingException>(() => checkout.GetTotal("ABCXD"));
        }
    }
}