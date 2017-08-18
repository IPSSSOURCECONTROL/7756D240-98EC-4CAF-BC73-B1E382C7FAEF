using System.Collections.Generic;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.User
{
    public class UserResponse : ServiceResponseBase
    {
        public UserResponse()
        {
        }

        public UserResponse(string message, ServiceResult serviceResult)
            :base(message, serviceResult)
        {

        }

        public UserAm User { get; set; }=new UserAm();

        public IEnumerable<UserAm> Users { get; set; }=new List<UserAm>();
    }
}