using System.Linq;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Application.Model;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Domain;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Serialization;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Application
{
    /// <summary>
    /// Base type for all 
    /// <see cref="ApplicationServiceBase{TServiceRequest,TServiceResponse,TRepository,TDomainEntityType,TApplicationModelType}"/> types.<para/>
    /// <para></para>
    /// <para>Makes available a <see cref="TRepository"/> for persistance needs.</para>
    /// <para>Makes available a <see cref="IDomainFactory{TDomainEntityType,TApplicationModelType}"/> 
    /// to create <see cref="TDomainEntityType"/>'s and <see cref="TApplicationModelType"/>'s</para>
    /// </summary>
    /// <typeparam name="TServiceResponse"></typeparam>
    /// <typeparam name="TRepository"></typeparam>
    /// <typeparam name="TDomainEntityType"></typeparam>
    /// <typeparam name="TApplicationModelType"></typeparam>
    /// <typeparam name="TServiceRequest"></typeparam>
    public class ApplicationServiceBase<
        TServiceRequest,
        TServiceResponse,
        TRepository, 
        TDomainEntityType,
        TApplicationModelType>
        : IRepositoryAvailable<TRepository>,
        IDomainFactoryAvailable<TDomainEntityType, TApplicationModelType>,
        IApplicationService<TServiceRequest, TServiceResponse> 
        where TServiceRequest : ServiceRequestBase<TApplicationModelType>
        where TRepository : class, IBasicRepository<TDomainEntityType> 
        //where TRepository: class 
        where TDomainEntityType : AggregateRoot, IEntity, IBusinessLink
        where TApplicationModelType : ApplicationModelBase, new()
        where TServiceResponse : ServiceResponseBase<TApplicationModelType>, new()
    {
        /// <summary>
        /// The <see cref="TRepository"/> providing 
        /// persistance support for the <see cref="ApplicationServiceBase{TServiceRequest,TServiceResponse,TRepository,TDomainEntityType,TApplicationModelType}
        /// TRepository,TDomainEntityType,TApplicationModelType}"/>
        /// </summary>
        [MandatoryInjection]
        public TRepository Repository { get; set; }

        /// <summary>
        /// Contain all data that is a result of the a service request. 
        /// <see cref="TServiceResponse"/>
        /// </summary>
        public TServiceResponse Response { get; } = new TServiceResponse();

        /// <summary>
        /// Factory type to build 
        /// <see cref="TApplicationModelType"/>'s and <see cref="TDomainEntityType"/>'s
        /// </summary>
        [MandatoryInjection]
        public IDomainFactory<TDomainEntityType, TApplicationModelType> DomainFactory { get; set; }

        [MandatoryInjection]
        public IObjectSerializer ObjectSerializer { get; set; }

        [AuthorizeAction]
        [ServiceRequestMethod]
        public virtual TServiceResponse GetById(TServiceRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.EntityId))
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(request.EntityId));
                return this.Response;
            }

            TDomainEntityType domainEntityType = this.Repository.GetById(request.EntityId);

            if (domainEntityType == null)
            {
                this.Response.RegisterError(MessageFormatter.RecordWithIdDoesNotExist(request.EntityId));
            }
            else
            {
                this.Response.ApplicationModel = this.DomainFactory.BuildApplicationModelType(domainEntityType);
                this.Response.RegisterSuccess();
            }

            return this.Response;
        }

        [AuthorizeAction]
        [ServiceRequestMethod]
        public virtual TServiceResponse GetAll(TServiceRequest request)
        {
            this.Response.ApplicationModels = this.DomainFactory.BuildApplicationModelTypes(
                this.Repository.GetAll().Where(x=>x.BusinessId == request.AuthorizationContext.BusinessId));

            this.Response.RegisterSuccess();
            return this.Response;
        }

        [AuthorizeAction]
        [ServiceRequestMethod]
        public virtual TServiceResponse Add(TServiceRequest request)
        {
            if (request.ApplicationModel == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    MessageFormatter.NormalizeApplicationModelName(typeof(TApplicationModelType))));
                return this.Response;
            }

            TDomainEntityType domainEntityType = this.DomainFactory.BuildDomainEntityType(request.ApplicationModel);

            this.Repository.Add(domainEntityType);

            this.Response.RegisterSuccess(
                MessageFormatter.EntitySuccessfullyAdded<TDomainEntityType>(domainEntityType.Id));

            return this.Response;
        }

        [AuthorizeAction]
        [ServiceRequestMethod]
        public virtual TServiceResponse Update(TServiceRequest request)
        {
            if (request.ApplicationModel == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    MessageFormatter.NormalizeApplicationModelName(typeof(TApplicationModelType))));
                return this.Response;
            }

            TDomainEntityType domainEntityType = this.DomainFactory.BuildDomainEntityType(request.ApplicationModel, false);

            this.Repository.Update(domainEntityType);

            this.Response.RegisterSuccess(
                MessageFormatter.EntitySuccessfullyUpdated<TDomainEntityType>(domainEntityType.Id));

            return this.Response;
        }

        [AuthorizeAction]
        [ServiceRequestMethod]
        public virtual TServiceResponse Delete(TServiceRequest request)
        {
            if (request.ApplicationModel == null)
            {
                this.Response.RegisterError(MessageFormatter.IsARequiredField(
                    nameof(request.ApplicationModel)));
                return this.Response;
            }

            TDomainEntityType domainEntityType = this.DomainFactory.BuildDomainEntityType(request.ApplicationModel, false);

            this.Repository.Delete(domainEntityType);

            this.Response.RegisterSuccess(
                MessageFormatter.EntitySuccessfullyRemoved<TDomainEntityType>(domainEntityType.Id));

            return this.Response;
        }
    }
}
