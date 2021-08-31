using Kata.ShoppingCart.Interfaces;
using Kata.ShoppingCart.Repositories;

namespace Kata.ShoppingCart
{
    public class Checkout : ICheckout
    {
        private readonly IProductRepository _productsRepo;
        private readonly IDiscountRepository _discountRepo;

        public Checkout(IProductRepository productRepo, IDiscountRepository discountRepo)
        {
            _productsRepo = productRepo;
            _discountRepo = discountRepo;
        }

        public decimal GetTotal(string productCodes)
        {
            var shoppingCart = new ShoppingCart();

            foreach (char code in productCodes)
            {
                var product = _productsRepo.GetOne(code);

                shoppingCart.AddProductToCart(product);
            }

            decimal cartTotal = 0;

            foreach (var cartItem in shoppingCart.GetItems())
            {
                var checkoutItem = new CheckoutItem(cartItem);

                var productDiscounts = _discountRepo.GetAllForProduct(cartItem.Product.ProductId);

                checkoutItem.ApplyDiscounts(productDiscounts);

                cartTotal += checkoutItem.ProductTotal;
            }

            return cartTotal;
        }
    }
}