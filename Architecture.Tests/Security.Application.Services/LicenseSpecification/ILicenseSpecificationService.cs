using Architecture.Tests.Infrustructure.Application;

namespace Architecture.Tests.Security.Application.Services.LicenseSpecification
{
    public interface ILicenseSpecificationService : IApplicationService<LicenseSpecificationServiceRequest, LicenseSpecificationResponse>
    {
    }
}