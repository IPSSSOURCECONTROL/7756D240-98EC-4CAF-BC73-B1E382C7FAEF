using System.Security.Principal;
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