using System;
using System.ComponentModel.DataAnnotations.Schema;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.DataModels
{
    [Table("License", Schema = "KBIT")]
    public class LicenseDM : DataModelBase
    {
        public DateTime Expires { get; set; }
        public DateTime ValidFrom { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
    }
}