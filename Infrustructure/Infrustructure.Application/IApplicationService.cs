using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Application
{
    public interface IApplicationService<TServiceRequest, TServiceResponse>
        where TServiceRequest : class 
        where TServiceResponse : class
    {
        [AuthorizeAction]
        [ServiceRequestMethod]
        TServiceResponse GetById(TServiceRequest request);

        [AuthorizeAction]
        [ServiceRequestMethod]
        TServiceResponse GetAll(TServiceRequest request);

        [AuthorizeAction]
        [ServiceRequestMethod]
        TServiceResponse Add(TServiceRequest request);

        [AuthorizeAction]
        [ServiceRequestMethod]
        TServiceResponse Update(TServiceRequest request);

        [AuthorizeAction]
        [ServiceRequestMethod]
        TServiceResponse Delete(TServiceRequest request);
    }
}
