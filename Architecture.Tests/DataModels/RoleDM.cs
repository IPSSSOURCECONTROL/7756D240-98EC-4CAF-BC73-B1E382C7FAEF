using System.ComponentModel.DataAnnotations.Schema;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.DataModels
{
    [Table("Role", Schema = "KBIT")]
    public class RoleDM : DataModelBase
    {
        public string Name { get; set; }
    }
}