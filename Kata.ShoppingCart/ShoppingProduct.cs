namespace Kata.ShoppingCart
{
    public class ShoppingProduct
    {
        public char ProductId { get; private set; }
        public decimal Price { get; private set; }

        public ShoppingProduct(char productId, decimal price)
        {
            ProductId = productId;
            Price = price;
        }
    }
}