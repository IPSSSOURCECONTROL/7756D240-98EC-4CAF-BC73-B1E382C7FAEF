namespace Architecture.Tests.Security.Domain.Role
{
    public class AdministratorRole : Role
    {
        /// <summary>
        /// Creates an <see cref="AdministratorRole"/> with an auto generated Id.
        /// </summary>
        public AdministratorRole()
        {
        }

        /// <summary>
        /// Instantiates a <see cref="AdministratorRole"/> type with a Unique ID. 
        /// Call this constructor when building the object from persistence.
        /// </summary>
        /// <param name="id">UID</param>
        public AdministratorRole(string id) : base(id)
        {
        }

        protected override string GetTypeName()
        {
            return this.GetType().Name;
        }
    }
}