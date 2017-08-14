using System.ComponentModel.DataAnnotations.Schema;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.DataModels
{
    [Table("Business", Schema = "kbit")]
    public class BusinessDM : DataModelBase
    {
        public string Name { get; set; }
        public string LAddress { get; set; }
        public string LContactDetail { get; set; }
        public string LBillingInformation { get; set; }
    }
}
