using System.Collections.Generic;
using System.Linq;
using Kata.ShoppingCart.Repositories;

namespace Kata.ShoppingCart.Tests.NUnit.Stubs
{
    public class TestProductRepository : IProductRepository
    {
        private readonly Dictionary<char, ShoppingProduct> _productsRepo = new Dictionary<char, ShoppingProduct>();
        
        public void Add(ShoppingProduct product)
        {
            if (!_productsRepo.ContainsKey(product.ProductId))
            {
                _productsRepo.Add(product.ProductId, product);
            }
        }

        public ShoppingProduct GetOne(char productId)
        {
            return _productsRepo.ContainsKey(productId) ? _productsRepo[productId] : null;
        }

        public IList<ShoppingProduct> GetMany()
        {
            return _productsRepo.Values.ToList();
        }
    }
}