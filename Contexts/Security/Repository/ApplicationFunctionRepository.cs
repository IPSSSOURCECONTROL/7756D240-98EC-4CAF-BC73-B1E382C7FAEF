using System.Linq;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Security.Domain.ApplicationFunction;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository
{
    public class ApplicationFunctionRepository: BasicRepositoryBase<ApplicationFunction>, IApplicationFunctionRepository
    {
        public ApplicationFunctionRepository(
            IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        [Transactional]
        [ValidateMethodArguments]
        public override void Add(ApplicationFunction entity)
        {
            if (this.DatabaseContext.TableForQuery<ApplicationFunction>().Any(x => x.Name == entity.Name))
                throw new KBitException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithNameAlreadyExists(entity.Name));

            this.ThrowErrorOnEntityExists<ApplicationFunction>(entity.Id);

            this.DatabaseContext.Add(entity);
        }
    }
}
