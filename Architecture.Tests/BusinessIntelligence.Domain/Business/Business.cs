using Architecture.Tests.Infrustructure.Domain;

namespace Architecture.Tests.BusinessIntelligence.Domain.Business
{
    public class Business : AggregateRoot
    {
        public Business(string name, Address address, 
            ContactDetails contactDetails, BillingInformation billingInformation)
        {
            this.Name = name;
            this.Address = address;
            this.ContactDetails = contactDetails;
            this.BillingInformation = billingInformation;
        }

        public string Name { get; }
        public Address Address { get; }
        public ContactDetails ContactDetails { get; }
        public BillingInformation BillingInformation { get; }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}