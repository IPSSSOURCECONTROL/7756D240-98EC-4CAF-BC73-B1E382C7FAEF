using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.Security.Domain.LicenseSpecification
{
    /// <summary>
    /// Exposes functions to persist and read <see cref="LicenseSpecification"/> types.
    /// </summary>
    public interface ILicenseSpecificationRepository: IBasicRepository<LicenseSpecification>, ISecurityContextAvailable
    {
    }
}
