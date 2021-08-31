namespace Kata.ShoppingCart.Interfaces
{
    public interface ICheckout
    {
        decimal GetTotal(string productCodes);
    }
}