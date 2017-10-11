using System;
using System.Collections.Generic;
using System.Linq;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.ProductListing
{
    public abstract class ProductListing: BusinessEntity
    {
        protected ProductListing(Business.Business business)
        {
            this.Business = business;
            this.DateTime = DateTime.Now;
        }

        public virtual string ProductListingUniqueIdentifier { get; private set; } = string.Empty;
        public Business.Business Business { get; private set; }
        public string CustomerId { get; private set; }
        public DateTime DateTime { get; private set; }
        public ICollection<ProductListingItem> ProductListingItems { get; private set; } = new List<ProductListingItem>();

        public void AssignCustomer(Customer.Customer customer)
        {
            if(customer == null)
                throw new InvalidOperationException("Can not assign invalid Customer to Product Listing.");

            this.CustomerId = customer.Id;
        }

        public void  AddProductListingItem(ProductListingItem productListingItem)
        {
            if(productListingItem == null)
                return;

            this.ProductListingItems.Add(productListingItem);
        }

        public void AssignUniqueIdentifier(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Can not assign an invalid product listing unique identifier.");
            }

            this.ProductListingUniqueIdentifier = value;
        }

        public decimal CalculateSubTotalForAllProducts()
        {
            return this.ProductListingItems.Sum(x => x.TotalAmount);
        }

        public decimal CalculateTotalDiscountForAllProducts()
        {
            return this.ProductListingItems.Sum(x => x.TotalDiscount);
        }

        public decimal CalculateTotalForAllProducts()
        {
            return 0;
        }

        public decimal CalculateTotalVatForAllProducts()
        {
            return this.ProductListingItems.Sum(x => x.TotalVat);
        }

        public void RemoveProduct(string id)
        {
            if (this.ProductListingItems.Any(x => x.Product.Id == id))
                this.ProductListingItems.Remove(this.ProductListingItems.First(x => x.Product.Id == id));
        }
    }
}