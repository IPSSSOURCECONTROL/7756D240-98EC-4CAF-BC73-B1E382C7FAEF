using System;
using System.Collections.Generic;
using AspNet.Identity.MongoDB;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;

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

        public virtual DateTimeOffset? LockoutEndDateUtc { get; set; }

        public virtual bool LockoutEnabled { get; set; }

        public virtual int AccessFailedCount { get; set; }

        public virtual bool TwoFactorEnabled { get; set; }

        public virtual ICollection<IdentityUserRole> Roles { get; set; }=new List<IdentityUserRole>();

        public virtual ICollection<IdentityUserClaim> Claims { get; set; }=new List<IdentityUserClaim>();

        public virtual ICollection<IdentityUserLogin> Logins { get; set; }=new List<IdentityUserLogin>();


        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}
