using System.IO;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Configuration;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Serialization;
using KhanyisaIntel.Kbit.Framework.Security.Domain;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Security.Application.Models;

namespace KhanyisaIntel.Kbit.Framework.Security.Application.Services.ApplicationFunction
{
    public class ApplicationFunctionService :
        ApplicationServiceBase<ApplicationFunctionResponse, 
            IApplicationFunctionRepository,
            Domain.ApplicationFunction.ApplicationFunction,
            ApplicationFunctionAm> ,IApplicationFunctionService
    {
        private readonly IObjectSerializer _objectSerializer;

        public ApplicationFunctionService(
            IObjectSerializer objectSerializer)
        {
            this._objectSerializer = objectSerializer;
        }

        [ServiceRequestMethod]
        public ApplicationFunctionResponse GetById(ApplicationFunctionServiceRequest request)
        {
            ApplicationFunctionResponse response = new ApplicationFunctionResponse();

            response.RegisterError(MessageFormatter.NotSupported(MethodBase.GetCurrentMethod()));

            return response;
        }

        [ServiceRequestMethod]
        public ApplicationFunctionResponse GetAll(ApplicationFunctionServiceRequest request)
        {
            ApplicationFunctionResponse response = new ApplicationFunctionResponse();

            response.RegisterError(MessageFormatter.NotSupported(MethodBase.GetCurrentMethod()));

            return response;
        }

        [ServiceRequestMethod]
        public ApplicationFunctionResponse Add(ApplicationFunctionServiceRequest request)
        {
            ApplicationFunctionResponse response = new ApplicationFunctionResponse();

            response.RegisterError(MessageFormatter.NotSupported(MethodBase.GetCurrentMethod()));

            return response;
        }

        [ServiceRequestMethod]
        public ApplicationFunctionResponse Update(ApplicationFunctionServiceRequest request)
        {
            ApplicationFunctionResponse response = new ApplicationFunctionResponse();

            if (string.IsNullOrWhiteSpace(request.ApplicationFunctionsConfigurationPath))
            {
                response.RegisterError(MessageFormatter.InvalidConfigurationFilePath(request.ApplicationFunctionsConfigurationPath));
                return response;
            }

            ApplicationFunctionsConfiguration applicationFunctionsConfiguration =
                this._objectSerializer.Deserialize<ApplicationFunctionsConfiguration>(File.ReadAllText(request
                    .ApplicationFunctionsConfigurationPath));

            foreach (Function function in applicationFunctionsConfiguration.Functions)
            {
                Domain.ApplicationFunction.ApplicationFunction applicationFunction =
                    SecurityDomianFactory.CreateApplicationFunction(function.Name, function.NameSpace);

                this.Repository.Add(applicationFunction);
            }

            response.RegisterSuccess("Successfully updated all application functions. ");

            return response;
        }

        [ServiceRequestMethod]
        public ApplicationFunctionResponse Delete(ApplicationFunctionServiceRequest request)
        {
            ApplicationFunctionResponse response = new ApplicationFunctionResponse();

            response.RegisterError(MessageFormatter.NotSupported(MethodBase.GetCurrentMethod()));

            return response;
        }
    }
}