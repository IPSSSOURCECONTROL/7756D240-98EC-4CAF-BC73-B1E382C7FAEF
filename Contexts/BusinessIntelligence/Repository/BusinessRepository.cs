using System.Linq;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Business;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository
{
    public class BusinessRepository : BasicRepositoryBase<Business>, IBusinessRepository
    {
        public BusinessRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        [Transactional]
        [ValidateMethodArguments]
        public override void Add(Business entity)
        {
            this.ThrowErrorOnEntityExists<Business>(entity.Id);

            if (this.DatabaseContext.Table<Business>()
                .Any(x => x.ContactDetails.Email == entity.ContactDetails.Email &&
                x.Id != entity.Id))
                throw new EntityAlreadyExistException(MethodBase.GetCurrentMethod(),
                    $"Email address '{entity.ContactDetails.Email}' already exists. Duplicate emails not allowed. " +
                    $"Another Business record is using the provided email address.");

            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        [ValidateMethodArguments]
        public override void Update(Business entity)
        {
            this.ThrowErrorOnEntityDoesNotExist<Business>(entity.Id);

            if (this.DatabaseContext.Table<Business>()
                .Any(x => x.ContactDetails.Email == entity.ContactDetails.Email && 
                x.Id != entity.Id))
                throw new EntityAlreadyExistException(MethodBase.GetCurrentMethod(),
                    $"Email address '{entity.ContactDetails.Email}' already exists. Duplicate emails not allowed. " +
                    $"Another Business record is using the provided email address.");

            this.DatabaseContext.Remove<Business>(entity.Id);
            this.DatabaseContext.Add(entity);
        }
    }
}