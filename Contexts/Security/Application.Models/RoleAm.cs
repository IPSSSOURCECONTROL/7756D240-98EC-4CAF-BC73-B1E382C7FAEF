using System.Collections.Generic;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application.Model;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Models
{
    public class RoleAm: ApplicationModelBase
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<ApplicationFunctionAm> ApplicationFunctions { get; set; } = new List<ApplicationFunctionAm>();
    }
}
