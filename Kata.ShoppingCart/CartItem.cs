namespace Kata.ShoppingCart
{
    public class CartItem
    {
        public ShoppingProduct Product { get; }
        public int Quantity { get; private set; }

        public CartItem(ShoppingProduct product)
        {
            Product = product;
            Quantity = 0;
        }

        public void AddItem(int quantity)
        {
            Quantity += quantity;
        }
    }
}