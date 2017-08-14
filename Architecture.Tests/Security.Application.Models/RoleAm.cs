using System.Collections.Generic;
using Architecture.Tests.Infrustructure.Application.Model;

namespace Architecture.Tests.Security.Application.Models
{
    public class RoleAm: ApplicationModelBase
    {
        public string Name { get; set; }

        public IEnumerable<ApplicationFunctionAm> ApplicationFunctions { get; set; } = new List<ApplicationFunctionAm>();
    }
}
