using Architecture.Tests.Infrustructure.Application;

namespace Architecture.Tests.Security.Application.Services.User
{
    public interface IUserService : IApplicationService<UserServiceRequest, UserResponse>
    {
    }
}