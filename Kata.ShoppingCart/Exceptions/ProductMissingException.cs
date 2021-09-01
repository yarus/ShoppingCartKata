using System;

namespace Kata.ShoppingCart.Exceptions
{
    public class ProductMissingException : Exception
    {
        public char ProductId { get; }

        public ProductMissingException(char productId)
        {
            ProductId = productId;
        }
    }
}
