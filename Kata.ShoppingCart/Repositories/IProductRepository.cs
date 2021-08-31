using System.Collections.Generic;

namespace Kata.ShoppingCart.Repositories
{
    public interface IProductRepository
    {
        void Add(ShoppingProduct product);
        ShoppingProduct GetOne(char productId);
        IList<ShoppingProduct> GetMany();
    }
}