using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.ApplicationFunction
{
    public class ApplicationFunctionServiceRequest : ServiceRequestBase<ApplicationFunctionAm>
    {
        public string ApplicationFunctionsConfigurationPath { get; set; } = string.Empty;
    }
}