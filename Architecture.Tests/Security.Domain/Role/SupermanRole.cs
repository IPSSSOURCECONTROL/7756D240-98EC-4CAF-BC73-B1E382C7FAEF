namespace Architecture.Tests.Security.Domain.Role
{
    public class SupermanRole: Role
    {
        public SupermanRole()
        {
        }

        public SupermanRole(string id) : base(id)
        {
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}
