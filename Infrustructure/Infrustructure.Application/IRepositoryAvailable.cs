using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Application
{
    /// <summary>
    /// Exposes a <see cref="TRepository"/> for implementing types.
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    public interface IRepositoryAvailable<TRepository> where TRepository : class
    {
        [MandatoryInjection]
        TRepository Repository { get; set; }
    }
}