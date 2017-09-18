using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Security.Application.Services.User;
using KhanyisaIntel.Kbit.Framework.Security.Domain.Role;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace Kbit.ControlCentre.IdentityExtensions
{
    public static class IdentityExtensions
    {
        public static IUserRepository UserRepository { get; set; }

        public static bool IsAdminRole(this IIdentity identity, string userRole)
        {
            if (userRole == "AdministratorRole" || userRole == "SupermanRole")
                return true;
            else
                return false;
        }
    }
}