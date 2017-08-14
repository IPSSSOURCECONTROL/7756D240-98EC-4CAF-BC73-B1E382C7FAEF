using System.ComponentModel.DataAnnotations.Schema;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.DataModels
{
    [Table("User", Schema = "KBIT")]
    public class UserDM: DataModelBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string LRole { get; set; }
        public string AccountStatus { get; set; }
        public string AccountStatusReason { get; set; }
    }
}
