using System;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;

namespace KhanyisaIntel.Kbit.Framework.Security.Domain.AspNet
{
    public class AspNetPrincipleUser: AggregateRoot
    {
        public virtual string UserName { get; set; }

        public virtual string LowerCaseUserName { get; set; }

        public virtual string EmailAddress { get; set; }

        public virtual string LowerCaseEmailAddress { get; set; }

        public virtual bool EmailAddressConfirmed { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual bool PhoneNumberConfirmed { get; set; }

        public virtual string PasswordHash { get; set; }

        public virtual string SecurityStamp { get; set; }

        //public virtual DateTimeOffset? LockoutEndDateUtc { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual int AccessFailedCount { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        //public virtual ICollection<IdentityUserRole> Roles { get; set; }=new List<IdentityUserRole>();

        //public virtual ICollection<IdentityUserClaim> Claims { get; set; }=new List<IdentityUserClaim>();

        //public virtual ICollection<IdentityUserLogin> Logins { get; set; }=new List<IdentityUserLogin>();

        public static AspNetPrincipleUser MapFrom(User.User user)
        {
            if(user == null)
                throw new KbitNullArgumentException(MethodBase.GetCurrentMethod(), 
                    $"Can not map null {nameof(User.User)}");

            AspNetPrincipleUser aspNetPrincipleUser = new AspNetPrincipleUser();
            aspNetPrincipleUser.Id = user.Id;
            aspNetPrincipleUser.EmailAddress = user.Email;
            aspNetPrincipleUser.LowerCaseEmailAddress = user.Email.ToLower();
            aspNetPrincipleUser.UserName = user.Email;
            aspNetPrincipleUser.LowerCaseUserName = user.Email;
            aspNetPrincipleUser.PasswordHash = user.Password.Value;
            aspNetPrincipleUser.SecurityStamp = Guid.NewGuid().ToString();
            aspNetPrincipleUser.AccessFailedCount = 0;

            return aspNetPrincipleUser;
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}
