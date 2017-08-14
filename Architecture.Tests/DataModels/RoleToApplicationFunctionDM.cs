using System.ComponentModel.DataAnnotations.Schema;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.DataModels
{
    [Table("RoleToApplicationFunction", Schema = "KBIT")]
    public class RoleToApplicationFunctionDM : DataModelBase
    {
        public string LRole { get; set; }
        public string LApplicationFunction { get; set; }
    }
}