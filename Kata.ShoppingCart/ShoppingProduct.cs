namespace Kata.ShoppingCart
{
    public class ShoppingProduct
    {
        public char ProductId { get; }
        public decimal Price { get; }

        public ShoppingProduct(char productId, decimal price)
        {
            ProductId = productId;
            Price = price;
        }
    }
}