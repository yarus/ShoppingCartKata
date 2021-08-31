using System;
using Kata.ShoppingCart.Interfaces;

namespace Kata.ShoppingCart.Discounts
{
    public class QuantityDiscount : IDiscount
    {
        public char ProductId { get; }
        public int OfferQuantity { get; }
        public decimal DiscountedPrice { get; }

        public QuantityDiscount(char productId, int offerQuantity, decimal discountedPrice)
        {
            ProductId = productId;
            OfferQuantity = offerQuantity;
            DiscountedPrice = discountedPrice;
        }

        public decimal GetDiscountedTotal(CheckoutItem item)
        {
            var baseTotal = item.ProductTotal;

            if (item.CartItem.Product.ProductId != ProductId || OfferQuantity > item.CartItem.Quantity)
            {
                return baseTotal;
            }

            var discountsCount = Math.Floor((decimal)item.CartItem.Quantity / OfferQuantity);

            var discountedProductsQuantity = discountsCount * OfferQuantity;

            var baseProductsQuantity = item.CartItem.Quantity - discountedProductsQuantity;

            var baseProductsTotal = item.ProductTotal * baseProductsQuantity / item.CartItem.Quantity;

            var discountedTotal = (discountsCount * DiscountedPrice) + baseProductsTotal;

            return discountedTotal;
        }
    }
}