namespace KhanyisaIntel.Kbit.Framework.Security.Domain.Role
{
    public class ManagerRole : Role
    {
        public ManagerRole()
        {
        }

        public ManagerRole(string id) : base(id)
        {
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}