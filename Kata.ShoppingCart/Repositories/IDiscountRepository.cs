using System.Collections.Generic;
using Kata.ShoppingCart.Discounts;

namespace Kata.ShoppingCart.Repositories
{
    public interface IDiscountRepository
    {
        void Add(IDiscount discount);
        IList<IDiscount> GetAllForProduct(char productId);
    }
}