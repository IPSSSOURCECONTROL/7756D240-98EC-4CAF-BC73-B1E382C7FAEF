namespace Architecture.Tests.Infrustructure.Application
{
    public class ApplicationServiceBase<TServiceResponse> where TServiceResponse : ServiceResponseBase, new ()
    {
        public TServiceResponse Response { get; } = new TServiceResponse();
    }
}
