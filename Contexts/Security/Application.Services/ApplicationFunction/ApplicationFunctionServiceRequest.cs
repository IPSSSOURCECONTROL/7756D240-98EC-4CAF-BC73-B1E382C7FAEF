using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.ApplicationFunction
{
    public class ApplicationFunctionServiceRequest : ServiceRequestBase
    {
        public string ApplicationFunctionsConfigurationPath { get; set; } = string.Empty;
    }
}