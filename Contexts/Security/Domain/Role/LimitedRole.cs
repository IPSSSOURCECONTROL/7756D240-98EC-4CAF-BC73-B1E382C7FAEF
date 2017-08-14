namespace KhanyisaIntel.Kbit.Framework.Security.Domain.Role
{
    public class LimitedRole : Role
    {
        public LimitedRole()
        {
        }

        public LimitedRole(string id) : base(id)
        {
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}