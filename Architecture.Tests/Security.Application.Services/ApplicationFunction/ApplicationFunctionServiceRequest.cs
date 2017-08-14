using Architecture.Tests.Infrustructure.Application;

namespace Architecture.Tests.Security.Application.Services.ApplicationFunction
{
    public class ApplicationFunctionServiceRequest : ServiceRequestBase
    {
        public string ApplicationFunctionsConfigurationPath { get; set; } = string.Empty;
    }
}