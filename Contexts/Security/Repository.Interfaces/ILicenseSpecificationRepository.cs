using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Security.Domain.LicenseSpecification;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces
{
    /// <summary>
    /// Exposes functions to persist and read <see cref="KhanyisaIntel.Kbit.Framework.Security.Domain.LicenseSpecification"/> types.
    /// </summary>
    public interface ILicenseSpecificationRepository: 
        IBasicRepository<LicenseSpecification>, ISecurityContextAvailable
    {
    }
}
