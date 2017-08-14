using System.Collections.Generic;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Domain
{
    public interface IDomainFactory<TDomainEntityType, TApplicationModelType>
        where TDomainEntityType : IEntity
        where TApplicationModelType: class
    { 
        [ValidateMethodArguments]
        TDomainEntityType BuildDomainEntityType(TApplicationModelType applicationModel);
        [ValidateMethodArguments]
        TApplicationModelType BuildApplicationModelType(TDomainEntityType domainEntity);
        [ValidateMethodArguments]
        IEnumerable<TApplicationModelType> BuildApplicationModelTypes(IEnumerable<TDomainEntityType> domainEntities);
    }
}
