using System;
using System.Collections.Generic;
using System.Linq;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.ProductListing;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Customer
{
    public class Customer: BusinessEntity
    {
        public Customer(string name, string code, Address address, 
            ContactDetails contactDetails, Representative representative, 
            BillingInformation billingInformation, string businessId):base(businessId)
        {
            this.Name = name;
            this.Code = code;
            this.Address = address;
            this.ContactDetails = contactDetails;
            this.Representative = representative;
            this.BillingInformation = billingInformation;
            this.AccountStatus = new Inactive();
            this.ProductListings = new List<ProductListing.ProductListing>();
        }

        public Customer(string name, string code, Address address,
            ContactDetails contactDetails,
            BillingInformation billingInformation, string businessId) : base(businessId)
        {
            this.Name = name;
            this.Code = code;
            this.Address = address;
            this.ContactDetails = contactDetails;
            this.BillingInformation = billingInformation;
            this.AccountStatus = new Inactive();
            this.ProductListings = new List<ProductListing.ProductListing>();
        }

        public string Name { get; set; }
        public string Code { get; private set; }
        public AccountStatus AccountStatus { get; private set; }
        public Address Address { get; private set; }
        public ContactDetails ContactDetails { get; private set; }
        public Representative Representative { get; private set; }
        public BillingInformation BillingInformation { get; private set; }

        public IEnumerable<ProductListing.ProductListing> ProductListings { get; private set; }

        public void Activate()
        {
            this.AccountStatus = new Active();
        }

        public void Deactivate()
        {
            this.AccountStatus = new Inactive();
        }

        public void AssignRepresentative(Representative representative)
        {
            if(representative == null)
                throw new ArgumentNullException("Can not set null representative.");

            this.Representative = representative;
        }

        public void AddProductListing(ProductListing.ProductListing productListing)
        {
            if(productListing == null)
                throw new InvalidOperationException($"Invalid Product Listing");

            productListing.AssignCustomer(this);

            List<ProductListing.ProductListing> costEstimateList = this.ProductListings.ToList();
            costEstimateList.Add(productListing);

            this.ProductListings = costEstimateList;
        }

        public IEnumerable<ProductListing.ProductListing> GetSpecificProductListingTypes<TProductListingType>() 
            where TProductListingType: ProductListing.ProductListing
        {
            return this.ProductListings.Where(x => x.GetType() == typeof(TProductListingType));
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}
