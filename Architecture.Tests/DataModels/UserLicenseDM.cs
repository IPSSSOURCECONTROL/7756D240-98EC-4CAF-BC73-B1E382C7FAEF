using System.ComponentModel.DataAnnotations.Schema;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.DataModels
{
    [Table("UserLicense", Schema = "KBIT")]
    public class UserLicenseDM : DataModelBase
    {
        public string LLicense { get; set; }
        public string LUser { get; set; }
    }
}