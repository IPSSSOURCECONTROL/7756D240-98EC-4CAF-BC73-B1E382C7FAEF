using Architecture.Tests.Infrustructure.Application;

namespace Architecture.Tests.Security.Application.Services.ApplicationFunction
{
    public interface IApplicationFunctionService : IApplicationService<ApplicationFunctionServiceRequest, ApplicationFunctionResponse>
    {
    }
}