using System.Collections.Generic;
using System.Linq;
using Kata.ShoppingCart.Discounts;
using Kata.ShoppingCart.Repositories;

namespace Kata.ShoppingCart.Tests.NUnit.Stubs
{
    public class TestDiscountRepository : IDiscountRepository
    {
        private readonly IList<IDiscount> _discounts = new List<IDiscount>();

        public void Add(IDiscount discount)
        {
            _discounts.Add(discount);
        }

        public IList<IDiscount> GetAllForProduct(char productId)
        {
            return _discounts.Where(x => x.ProductId == productId).ToList();
        }
    }
}