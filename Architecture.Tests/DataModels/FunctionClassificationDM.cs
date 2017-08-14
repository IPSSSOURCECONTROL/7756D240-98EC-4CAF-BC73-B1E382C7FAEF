using System.ComponentModel.DataAnnotations.Schema;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.DataModels
{
    [Table("FunctionClassification", Schema = "KBIT")]
    public class FunctionClassificationDM : DataModelBase
    {
        public string Name { get; set; }
    }
}