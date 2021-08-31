using System.Collections.Generic;
using Kata.ShoppingCart.Discounts;

namespace Kata.ShoppingCart
{
    public class CheckoutItem
    {
        public CartItem CartItem { get; }
        public decimal ProductTotal { get; private set; }

        public CheckoutItem(CartItem cartItem)
        {
            CartItem = cartItem;
            ProductTotal = cartItem.Product.Price * cartItem.Quantity;
        }

        public void ApplyDiscounts(IList<IDiscount> discounts)
        {
            foreach (var discount in discounts)
            {
                ProductTotal = discount.GetDiscountedTotal(this);
            }
        }
    }
}