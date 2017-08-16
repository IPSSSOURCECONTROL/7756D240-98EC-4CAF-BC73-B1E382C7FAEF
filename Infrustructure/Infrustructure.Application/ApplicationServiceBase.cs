using System;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Application
{
    /// <summary>
    /// Base type for all 
    /// <see cref="ApplicationServiceBase{TServiceResponse,TRepository,TDomainEntityType,TApplicationModelType}"/> types.<para/>
    /// <para></para>
    /// <para>Makes available a <see cref="TRepository"/> for persistance needs.</para>
    /// <para>Makes available a <see cref="IDomainFactory{TDomainEntityType,TApplicationModelType}"/> 
    /// to create <see cref="TDomainEntityType"/>'s and <see cref="TApplicationModelType"/>'s</para>
    /// </summary>
    /// <typeparam name="TServiceResponse"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    /// <typeparam name="TDomainEntityType"></typeparam>
    /// <typeparam name="TApplicationModelType"></typeparam>
    public class ApplicationServiceBase<TServiceResponse, TRepository, TDomainEntityType, TApplicationModelType> 
        : IRepositoryAvailable<TRepository>, 
        IDomainFactoryAvailable<TDomainEntityType, TApplicationModelType>
        where TRepository : class
        where TDomainEntityType: IEntity
        where TApplicationModelType: class 
        where TServiceResponse : ServiceResponseBase, new()
    {
        /// <summary>
        /// The <see cref="TRepository"/> providing 
        /// persistance support for the <see cref="ApplicationServiceBase{TServiceResponse,TRepository,TDomainEntityType,TApplicationModelType}"/>
        /// </summary>
        [MandatoryInjection]
        public TRepository Repository { get; set; }

        /// <summary>
        /// <see cref="TServiceResponse"/>
        /// </summary>
        public TServiceResponse Response { get; } = new TServiceResponse();

        /// <summary>
        /// Factory type to build 
        /// <see cref="TApplicationModelType"/>'s and <see cref="TDomainEntityType"/>'s
        /// </summary>
        [MandatoryInjection]
        public IDomainFactory<TDomainEntityType, TApplicationModelType> DomainFactory { get; set; }
    }
}
