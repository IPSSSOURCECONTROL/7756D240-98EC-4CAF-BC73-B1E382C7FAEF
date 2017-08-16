using System;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Application
{
    /// <summary>
    /// Base type for all <see cref="ApplicationServiceBase{TServiceResponse,TRepository}"/> types.
    /// </summary>
    /// <typeparam name="TServiceResponse"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    public class ApplicationServiceBase<TServiceResponse, TRepository> : IRepositoryAvailable<TRepository> 
        where TRepository : class 
        where TServiceResponse : ServiceResponseBase, new()
    {
        /// <summary>
        /// The <see cref="TRepository"/> providing 
        /// persistance support for the <see cref="ApplicationServiceBase{TServiceResponse,TRepository}"/>
        /// </summary>
        [MandatoryInjection]
        public TRepository Repository { get; set; }

        /// <summary>
        /// <see cref="TServiceResponse"/>
        /// </summary>
        public TServiceResponse Response { get; } = new TServiceResponse();
    }
}
