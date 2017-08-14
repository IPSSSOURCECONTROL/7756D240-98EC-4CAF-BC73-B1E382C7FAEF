using System.ComponentModel.DataAnnotations.Schema;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.DataModels
{
    [Table("BillingInformation", Schema = "KBIT")]
    public class BillingInformationDM:DataModelBase
    {
        public string Bank { get; set; }
        public string AccountNumber { get; set; }
        public string BranchCode { get; set; }
        public string Reference { get; set; }
    }
}