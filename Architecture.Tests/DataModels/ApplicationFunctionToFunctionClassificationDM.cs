using System.ComponentModel.DataAnnotations.Schema;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.DataModels
{
    [Table("ApplicationFunctionToFunctionClassification", Schema = "KBIT")]
    public class ApplicationFunctionToFunctionClassificationDM : DataModelBase
    {
        public string LFunctionClassification { get; set; }
        public string LApplicationFunction { get; set; }
    }
}