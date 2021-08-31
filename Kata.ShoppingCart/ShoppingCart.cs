using System.Collections.Generic;
using System.Linq;
using Kata.ShoppingCart.Interfaces;

namespace Kata.ShoppingCart
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly Dictionary<char, CartItem> _items = new Dictionary<char, CartItem>();
        
        public void AddProductToCart(ShoppingProduct product)
        {
            if (!_items.ContainsKey(product.ProductId))
            {
                AddNewProductToCart(product);
            }

            IncreaseProductQuantity(product.ProductId);
        }

        public IList<CartItem> GetItems()
        {
            return _items.Values.ToList();
        }
        
        private void IncreaseProductQuantity(char productId)
        {
            _items[productId].AddItem(1);
        }

        private void AddNewProductToCart(ShoppingProduct product)
        {
            var cartItem = new CartItem(product);

            _items.Add(product.ProductId, cartItem);
        }
    }
}