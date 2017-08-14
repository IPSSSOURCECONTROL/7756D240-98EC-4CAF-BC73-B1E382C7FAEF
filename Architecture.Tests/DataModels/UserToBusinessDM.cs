using System.ComponentModel.DataAnnotations.Schema;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.DataModels
{
    [Table("UserToBusiness", Schema = "KBIT")]
    public class UserToBusinessDM: DataModelBase
    {
        public string LUser { get; set; }
        public string LBusiness { get; set; }
    }
}
