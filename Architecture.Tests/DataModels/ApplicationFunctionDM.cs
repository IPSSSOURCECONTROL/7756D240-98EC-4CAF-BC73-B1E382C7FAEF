using System.ComponentModel.DataAnnotations.Schema;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.DataModels
{
    [Table("ApplicationFunction", Schema = "KBIT")]
    public class ApplicationFunctionDM : DataModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        //public string RequireDualAuthorization { get; set; }
    }
}