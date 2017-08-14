using System;
using System.ComponentModel.DataAnnotations.Schema;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.DataModels
{
    [Table("Password", Schema = "KBIT")]
    public class PasswordDM : DataModelBase
    {
        public string Value { get; set; }
        public string PasswordResetPolicy { get; set; }
        public DateTime NextResetDate { get; set; }
        public string LUser { get; set; }
    }
}