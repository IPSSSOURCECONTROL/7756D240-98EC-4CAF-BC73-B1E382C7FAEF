using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.ApplicationFunction
{
    public class ApplicationFunctionResponse : ServiceResponseBase<ApplicationFunctionAm>
    {
        public ApplicationFunctionResponse()
        {
        }

        public ApplicationFunctionResponse(string message, ServiceResult serviceResult): base(message, serviceResult)
        {
        }
    }
}