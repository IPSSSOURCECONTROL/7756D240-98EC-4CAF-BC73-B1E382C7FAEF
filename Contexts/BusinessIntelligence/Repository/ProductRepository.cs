using System.Collections.Generic;
using System.Linq;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Domain.Product;
using KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using System.Reflection;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Exception;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Utilities;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Workflow.Exceptions;

namespace KhanyisaIntel.Kbit.Framework.BusinessIntelligence.Repository
{
    public class ProductRepository : BasicRepositoryBase, IProductRepository
    {
        public ProductRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Add(Product entity)
        {
            if (this.DatabaseContext.Table<Product>().Any(x => x.Id == entity.Id))
                throw new EntityAlreadyExistException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithIdAlreadyExists(entity.Id));


            this.DatabaseContext.Add(entity);
        }

        [Transactional]
        [ValidateMethodArguments]
        public void Delete(Product entity)
        {
            if (!this.DatabaseContext.Table<Product>().Any(x => x.Id == entity.Id))
                throw new EntityDoesNotExistException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithIdDoesNotExist(entity.Id));

            this.DatabaseContext.Remove<Product>(entity.Id);
        }

        public IEnumerable<Product> GetAll()
        {
            return this.DatabaseContext.Table<Product>();
        }

        public Product GetById(string id)
        {
            return this.DatabaseContext.TableForQuery<Product>().FirstOrDefault(x => x.Id == id);
        }

        [ValidateMethodArguments]
        public bool IsExist(Product entity)
        {
            return this.DatabaseContext.Table<Product>().Any(x => x.Id == entity.Id);
        }

        public void Update(Product entity)
        {
            if (!this.DatabaseContext.TableForQuery<Product>().Any(x => x.Id == entity.Id))
                throw new EntityDoesNotExistException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithIdDoesNotExist(entity.Id));

            this.DatabaseContext.Remove<Product>(entity.Id);
            this.DatabaseContext.Add(entity);
        }

    }
}
