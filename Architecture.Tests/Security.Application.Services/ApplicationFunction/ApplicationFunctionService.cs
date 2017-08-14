using System.IO;
using System.Reflection;
using Architecture.Tests.Infrustructure.AOP.Attributes;
using Architecture.Tests.Infrustructure.Configuration;
using Architecture.Tests.Infrustructure.Serialization;
using Architecture.Tests.Infrustructure.Utilities;
using Architecture.Tests.Security.Domain;
using Architecture.Tests.Security.Domain.ApplicationFunction;

namespace Architecture.Tests.Security.Application.Services.ApplicationFunction
{
    public class ApplicationFunctionService : IApplicationFunctionService
    {
        private readonly IApplicationFunctionRepository _applicationFunctionRepository;
        private readonly IObjectSerializer _objectSerializer;

        public ApplicationFunctionService(
            IApplicationFunctionRepository applicationFunctionRepository, 
            IObjectSerializer objectSerializer)
        {
            this._applicationFunctionRepository = applicationFunctionRepository;
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

                this._applicationFunctionRepository.Add(applicationFunction);
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