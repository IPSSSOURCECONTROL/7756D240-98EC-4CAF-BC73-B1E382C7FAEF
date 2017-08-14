using System.ComponentModel.DataAnnotations.Schema;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.DataModels
{
    [Table("BusinessLicenses", Schema = "KBIT")]
    public class BusinessLicensesDM : DataModelBase
    {
        public string LBusiness { get; set; }
        public string LLicense { get; set; }
    }
}