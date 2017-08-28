using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Customer;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces
{
    /// <summary>
    /// Exposes functions to persist and retrieve the <see cref="Customer"/> 
    /// <see cref="AggregateRoot"/> type.
    /// </summary>
    public interface ICustomerRepository : IBasicRepository<Customer>
    {

    }
}