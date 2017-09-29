using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Exceptions;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.ProductListing
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
        public Product.Product Product { get; private set; }

        public void ApplyDiscount(decimal discount)
        {
            this.Discount = discount;
        }

        public decimal CalculateAmount()
        {
            return this.Product.PricingClassification.Rate * this.Quantity;
        }

        public decimal CalculateTotalDiscount()
        {
            decimal discount = this.CalculateAmount() * (this.Discount/100);
            return this.CalculateAmount() - discount;
        }
    }
}