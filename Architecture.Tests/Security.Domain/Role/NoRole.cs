namespace Architecture.Tests.Security.Domain.Role
{
    public class NoRole : Role
    {
        public NoRole()
        {
        }

        public NoRole(string id) : base(id)
        {
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}