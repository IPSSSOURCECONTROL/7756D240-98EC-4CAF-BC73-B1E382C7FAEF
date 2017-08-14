using System.ComponentModel.DataAnnotations.Schema;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.DataModels
{
    [Table("Address", Schema = "KBIT")]
    public class AddressDM:DataModelBase
    {
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string TownOrCity { get; set; }
        public string PostalCode { get; set; }
    }
}