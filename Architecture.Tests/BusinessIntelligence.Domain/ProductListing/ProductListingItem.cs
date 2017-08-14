using Architecture.Tests.BusinessIntelligence.Domain.Exceptions;

namespace Architecture.Tests.BusinessIntelligence.Domain.ProductListing
{
    public class ProductListingItem
    {
        public ProductListingItem(Product.Product product)
        {

            if (product == null)
                throw new CannotAddNullProductToProductListingException(nameof(product));

            this.Product = product;
        }

        public decimal Discount { get; private set; }
        public int Quantity { get; set; }
        public Product.Product Product { get; }

        public void ApplyDiscount(decimal discount)
        {
            this.Discount = discount;
        }

        public decimal CalculateAmount()
        {
            return 0;
        }

        public decimal CalculateTotalDiscount()
        {
            return 0;
        }
    }
}