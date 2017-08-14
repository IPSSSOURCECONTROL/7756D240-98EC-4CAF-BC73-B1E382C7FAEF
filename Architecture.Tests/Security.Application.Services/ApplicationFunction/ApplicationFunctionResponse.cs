using Architecture.Tests.Infrustructure.Application;

namespace Architecture.Tests.Security.Application.Services.ApplicationFunction
{
    public class ApplicationFunctionResponse : ServiceResponseBase
    {
        public ApplicationFunctionResponse()
        {
        }

        public ApplicationFunctionResponse(string message, ServiceResult serviceResult): base(message, serviceResult)
        {
        }
    }
}