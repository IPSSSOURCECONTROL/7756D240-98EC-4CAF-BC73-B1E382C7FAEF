using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Security.Domain.AspNet;
using KhanyisaIntel.Kbit.Framework.Security.Domain.LicenseSpecification;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces
{
    public interface IAspNetPrincipleUserRepository:
                IBasicRepository<AspNetPrincipleUser>, ISecurityContextAvailable
    {
    }
}
