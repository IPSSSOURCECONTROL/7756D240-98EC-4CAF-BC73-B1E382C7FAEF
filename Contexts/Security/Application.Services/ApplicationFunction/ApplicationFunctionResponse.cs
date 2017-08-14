using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.ApplicationFunction
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