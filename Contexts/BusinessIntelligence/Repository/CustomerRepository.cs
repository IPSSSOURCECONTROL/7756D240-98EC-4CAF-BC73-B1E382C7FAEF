using System.Linq;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Customer;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Security.Domain.User;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository
{
    public class CustomerRepository: BasicRepositoryBase<Customer>, ICustomerRepository
    {
        private readonly IUserRepository _userRepository;

        public CustomerRepository(IDatabaseContext databaseContext, 
            IUserRepository repository) : base(databaseContext)
        {
            this._userRepository = repository;
        }

        [Transactional]
        [ValidateMethodArguments]
        public override void Add(Customer entity)
        {
            this.ThrowErrorOnEntityExists<Customer>(entity.Id);

            if(this.DatabaseContext.Table<Customer>()
                .Any(x=>x.Code == entity.Code && x.Id != entity.Id))
                throw new KBitException(MethodBase.GetCurrentMethod(), 
                    $"Customer Code {entity.Code} already exists. ");

            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        [ValidateMethodArguments]
        public override void Update(Customer entity)
        {
            this.ThrowErrorOnEntityDoesNotExist<Customer>(entity.Id);

            if (this.DatabaseContext.Table<Customer>()
                .Any(x => x.Code == entity.Code && x.Id != entity.Id))
                throw new KBitException(MethodBase.GetCurrentMethod(),
                    $"Customer Code {entity.Code} already exists. ");

            this.DatabaseContext.Remove<Customer>(entity.Id);
            this.DatabaseContext.Add(entity);
        }

        public Representative GetRepresentativeById(string id)
        {
            User user = this._userRepository.GetById(id);

            if (user == null)
                throw new  KbitNullArgumentException(MethodBase.GetCurrentMethod(), 
                    $"Representative with Id {id} does not exist.");

            return new Representative(id, user.Name, user.Code);
        }
    }
}
