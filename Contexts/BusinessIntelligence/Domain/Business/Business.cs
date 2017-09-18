using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Business
{
    public class Business : BusinessEntity
    {
        public Business(string name, Address address, 
            ContactDetails contactDetails, 
            BillingInformation billingInformation)
        {
            this.Name = name;
            this.Address = address;
            this.ContactDetails = contactDetails;
            this.BillingInformation = billingInformation;
        }

        public string Name { get; private set; }
        public Address Address { get; private set; }
        public ContactDetails ContactDetails { get; private set; }
        public BillingInformation BillingInformation { get; private set; }
        public bool IsActive { get; private set; } = true;

        public void SetStatus(bool value)
        {
            this.IsActive = value;
        }

        public void Activate()
        {
            this.IsActive = false;
        }

        public void Deactivate()
        {
            this.IsActive = true;
        }

        public override string BusinessId
        {
            get { return this.Id; }
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}