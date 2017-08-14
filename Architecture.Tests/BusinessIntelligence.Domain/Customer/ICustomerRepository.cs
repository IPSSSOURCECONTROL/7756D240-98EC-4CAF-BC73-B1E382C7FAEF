using Architecture.Tests.Infrustructure.Domain;
using Architecture.Tests.Infrustructure.Repository;

namespace Architecture.Tests.BusinessIntelligence.Domain.Customer
{
    /// <summary>
    /// Exposes functions to persist and retrieve the <see cref="Customer"/> 
    /// <see cref="AggregateRoot"/> type.
    /// </summary>
    public interface ICustomerRepository : IBasicRepository<Customer>
    {

    }
}