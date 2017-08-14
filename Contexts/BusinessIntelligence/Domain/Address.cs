using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain
{
    public class Address: IValueObject
    {
        public Address(string addressLineOne, string addressLineTwo, 
            string street, string suburb, 
            string townOrCity, string postalCode)
        {
            this.AddressLineOne = addressLineOne;
            this.AddressLineTwo = addressLineTwo;
            this.Street = street;
            this.Suburb = suburb;
            this.TownOrCity = townOrCity;
            this.PostalCode = postalCode;
        }

        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string TownOrCity { get; set; }
        public string PostalCode { get; set; }
    }
}