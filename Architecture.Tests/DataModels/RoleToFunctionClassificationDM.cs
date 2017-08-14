using System.ComponentModel.DataAnnotations.Schema;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.DataModels
{
    [Table("RoleToFunctionClassification", Schema = "KBIT")]
    public class RoleToFunctionClassificationDM : DataModelBase
    {
        public string LRole { get; set; }
        public string LFunctionClassification { get; set; }
        public string DualAuthorizationClassification { get; set; }
        public bool IsEnabled { get; set; }
    }
}