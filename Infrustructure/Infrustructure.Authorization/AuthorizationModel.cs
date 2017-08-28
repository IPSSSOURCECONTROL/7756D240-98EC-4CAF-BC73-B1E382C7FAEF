using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Authorization
{
    public class AuthorizationModel
    {
        private readonly IUserRepository _userRepository;

        public AuthorizationModel(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }


    }
}
