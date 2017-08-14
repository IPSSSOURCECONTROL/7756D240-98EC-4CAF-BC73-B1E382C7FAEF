namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Workflow
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
        //public Role Role { get; set; } = new SupermanRole();
        public string BusinessId { get; set; }
    }
}