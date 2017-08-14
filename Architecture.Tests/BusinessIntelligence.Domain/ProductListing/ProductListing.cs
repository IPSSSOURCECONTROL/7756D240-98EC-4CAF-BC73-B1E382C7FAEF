using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.Tests.Infrustructure.Domain;

namespace Architecture.Tests.BusinessIntelligence.Domain.ProductListing
{
    public abstract class ProductListing: AggregateRoot
    {
        protected ProductListing(Business.Business business, Customer.Customer customer)
        {
            this.Business = business;
            this.Customer = customer;
        }

        public Business.Business Business { get; }
        public Customer.Customer Customer { get; }
        public DateTime DateTime { get; } = DateTime.Now;
        public ICollection<ProductListingItem> ProductListingItems { get; } = new List<ProductListingItem>();

        public void  AddProductListingItem(ProductListingItem productListingItem)
        {
            if(productListingItem == null)
                return;

            this.ProductListingItems.Add(productListingItem);
        }

        public decimal CalculateSubTotalForAllProducts()
        {
            return 0;
        }

        public decimal CalculateTotalDiscountForAllProducts()
        {
            return 0;
        }

        public decimal CalculateTotalForAllProducts()
        {
            return 0;
        }

        public decimal CalculateTotalVatForAllProducts()
        {
            return 0;
        }

        public void RemoveProduct(string productCode)
        {
            if (this.ProductListingItems.Any(x => x.Product.Id == productCode))
                this.ProductListingItems.Remove(this.ProductListingItems.First(x => x.Product.Id == productCode));
        }
    }
}