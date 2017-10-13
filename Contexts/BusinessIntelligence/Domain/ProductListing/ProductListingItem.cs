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

        public decimal TotalAmount { get; private set; }
        public decimal TotalDiscount { get; private set; }
        public decimal TotalVat { get; private set; }
        public decimal Discount { get; private set; }
        public int Quantity { get; private set; }
        public Product.Product Product { get; private set; }

        public void CalculateAmount(int quantity, decimal discount)
        {
            this.Discount = discount;
            this.Quantity = quantity;
            this.TotalAmount = this.Product.CalculateTotalAmount(quantity, discount);
        }

        public void CalculateTotalDiscount(int quantity, decimal discount)
        {
            this.Discount = discount;
            this.Quantity = quantity;
            this.TotalDiscount = this.Product.CalculateTotalDiscount(quantity, discount);
        }

        public void CalculateTotalVat(int quantity, decimal discount)
        {
            this.Discount = discount;
            this.Quantity = quantity;
            this.TotalVat = this.Product.CalculateTotalVat(quantity, discount);
        }
    }
}