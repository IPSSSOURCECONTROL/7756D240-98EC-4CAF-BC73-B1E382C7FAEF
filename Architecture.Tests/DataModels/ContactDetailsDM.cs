using System.ComponentModel.DataAnnotations.Schema;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.DataModels
{
    [Table("ContactDetails", Schema = "KBIT")]
    public class ContactDetailsDM:DataModelBase
    {
        public string Email { get; set; }
        public string TelephoneNumber { get; set; }
        public string CellphoneNumber { get; set; }
    }
}