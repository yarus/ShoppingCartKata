using System.Linq;
using Kata.ShoppingCart.Exceptions;
using NUnit.Framework;

namespace Kata.ShoppingCart.Tests.NUnit
{
    public class ShoppingCartTests
    {
        private ShoppingCart _cart;

        [SetUp]
        public void Setup()
        {
            _cart = new ShoppingCart();
        }
        
        [Test]
        public void ShoppingCart_GetItems_Should_Return0_When_Empty()
        {
            var items = _cart.GetItems();

            Assert.AreEqual(0, items.Count);
        }

        [Test]
        public void ShoppingCart_GetItems_Should_Return_Count()
        {
            _cart.AddProductToCart(new ShoppingProduct('A', 30));
            _cart.AddProductToCart(new ShoppingProduct('B', 40));
            _cart.AddProductToCart(new ShoppingProduct('C', 50));

            var items = _cart.GetItems();

            Assert.AreEqual(3, items.Count);
        }

        [Test]
        public void ShoppingCart_GetItems_Should_Return_Quantity()
        {
            _cart.AddProductToCart(new ShoppingProduct('A', 30));
            _cart.AddProductToCart(new ShoppingProduct('B', 40));
            _cart.AddProductToCart(new ShoppingProduct('A', 30));

            var items = _cart.GetItems();

            Assert.AreEqual(2, items.Count);

            var cartItem = items.FirstOrDefault(x => x.Product.ProductId == 'A');

            Assert.AreEqual(2, cartItem.Quantity);
        }
    }
}