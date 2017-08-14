using Architecture.Tests.Security.Domain.Role;

namespace Architecture.Tests.Infrustructure.Workflow
{
    public class AuthorizationContext
    {
        private string _userId;

        public string UserId {
            get
            {

                return this._userId;
            }
            set { this._userId = value; } }
        public Role Role { get; set; } = new SupermanRole();
        public string BusinessId { get; set; }
    }
}