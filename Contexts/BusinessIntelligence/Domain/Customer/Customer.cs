using System;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Customer
{
    public class Customer: BusinessEntity
    {
        public Customer(string name, Address address, 
            ContactDetails contactDetails, Representative representative, 
            BillingInformation billingInformation, string businessId):base(businessId)
        {
            this.Name = name;
            this.Address = address;
            this.ContactDetails = contactDetails;
            this.Representative = representative;
            this.BillingInformation = billingInformation;
            this.AccountStatus = new Inactive();
        }

        public Customer(string name, Address address,
            ContactDetails contactDetails,
            BillingInformation billingInformation, string businessId) : base(businessId)
        {
            this.Name = name;
            this.Address = address;
            this.ContactDetails = contactDetails;
            this.BillingInformation = billingInformation;
            this.AccountStatus = new Inactive();
        }

        public string Name { get; set; }
        public AccountStatus AccountStatus { get; private set; }
        public Address Address { get; private set; }
        public ContactDetails ContactDetails { get; private set; }
        public Representative Representative { get; private set; }
        public BillingInformation BillingInformation { get; private set; }

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

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}
