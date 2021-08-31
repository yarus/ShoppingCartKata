using System.Collections.Generic;

namespace Kata.ShoppingCart.Interfaces
{
    public interface IShoppingCart
    {
        void AddProductToCart(ShoppingProduct product);
        IList<CartItem> GetItems();
    }
}