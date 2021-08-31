namespace Kata.ShoppingCart.Discounts
{
    public interface IDiscount
    {
        char ProductId { get; }
        decimal GetDiscountedTotal(CheckoutItem item);
    }
}