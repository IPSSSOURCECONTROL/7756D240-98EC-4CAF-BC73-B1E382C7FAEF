using Architecture.Tests.Infrustructure.Domain;

namespace Architecture.Tests.BusinessIntelligence.Domain.Customer
{
    public class Customer: AggregateRoot
    {
        public Customer(string name, Address address, ContactDetails contactDetails, 
            Representative representative, BillingInformation billingInformation)
        {
            this.Name = name;
            this.Address = address;
            this.ContactDetails = contactDetails;
            this.Representative = representative;
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

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}
