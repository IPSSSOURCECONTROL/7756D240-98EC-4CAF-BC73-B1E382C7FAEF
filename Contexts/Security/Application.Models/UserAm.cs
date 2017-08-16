using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application.Model;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Models
{
    public class UserAm: ApplicationModelBase
    {
        [KbitRequired]
        public string Name { get; set; }

        public string Code { get; set; }

        [KbitRequired]
        public string Email { get; set; }

        [KbitRequired]
        public string Password { get; set; }

        [KbitRequired]
        public string AccountStatus { get; set; }
    }
}
