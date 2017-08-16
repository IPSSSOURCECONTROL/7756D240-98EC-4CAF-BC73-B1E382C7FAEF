using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Application
{
    /// <summary>
    /// Exposes a <see cref="IDomainFactory{TDomainEntityType,TApplicationModelType}"/> for 
    /// implementing  types.
    /// </summary>
    /// <typeparam name="TDomainEntityType"></typeparam>
    /// <typeparam name="TApplicationModelType"></typeparam>
    public interface IDomainFactoryAvailable<TDomainEntityType, TApplicationModelType>
        where TDomainEntityType : IEntity
        where TApplicationModelType: class 
    {
        /// <summary>
        /// Factory type to build 
        /// <see cref="TApplicationModelType"/>'s and <see cref="TDomainEntityType"/>'s
        /// </summary>
        IDomainFactory<TDomainEntityType, TApplicationModelType> DomainFactory { get; set; }
    }
}