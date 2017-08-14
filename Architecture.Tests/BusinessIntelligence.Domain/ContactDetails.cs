using Architecture.Tests.Infrustructure.Domain;

namespace Architecture.Tests.BusinessIntelligence.Domain
{
    public class ContactDetails: IValueObject
    {
        public ContactDetails(string email, string telephoneNumber, string cellphoneNumber)
        {
            this.Email = email;
            this.TelephoneNumber = telephoneNumber;
            this.CellphoneNumber = cellphoneNumber;
        }

        public string Email { get; }
        public string TelephoneNumber { get; }
        public string CellphoneNumber { get; }
    }
}