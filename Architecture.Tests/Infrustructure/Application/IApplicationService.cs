using Architecture.Tests.Infrustructure.AOP.Attributes;

namespace Architecture.Tests.Infrustructure.Application
{
    public interface IApplicationService<TServiceRequest, TServiceResponse>
        where TServiceRequest : class where TServiceResponse : class
    {
        [ServiceRequestMethod]
        TServiceResponse GetById(TServiceRequest request);

        [ServiceRequestMethod]
        TServiceResponse GetAll(TServiceRequest request);

        [ServiceRequestMethod]
        TServiceResponse Add(TServiceRequest request);

        [ServiceRequestMethod]
        TServiceResponse Update(TServiceRequest request);

        [ServiceRequestMethod]
        TServiceResponse Delete(TServiceRequest request);
    }
}
