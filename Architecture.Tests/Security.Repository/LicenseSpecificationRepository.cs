using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Architecture.Tests.Infrustructure.MongoDb;
using Architecture.Tests.Infrustructure.Reflection;
using Architecture.Tests.Infrustructure.Repository;
using Architecture.Tests.Infrustructure.Utilities;
using Architecture.Tests.Infrustructure.Validation;
using Architecture.Tests.Security.Domain.LicenseSpecification;

namespace Architecture.Tests.Security.Repository
{
    public class LicenseSpecificationRepository : BasicRepositoryBase, ILicenseSpecificationRepository
    {
        private readonly IObjectActivator _objectActivator;

        public LicenseSpecificationRepository(IDatabaseContext databaseContext, IObjectActivator objectActivator) : base(databaseContext)
        {
            this._objectActivator = objectActivator;
        }

        public void Add(LicenseSpecification entity)
        {
            Validator.CheckReferenceTypeForNull(entity, nameof(entity), MethodBase.GetCurrentMethod(), this.GetType());

            if (this.DatabaseContext.TableForQuery<LicenseSpecification>()
                .Any(x => x.Id == entity.Id))
            {
                throw new EntityAlreadyExistException(MethodBase.GetCurrentMethod(),
                    MessageFormatter.RecordWithIdAlreadyExists(entity.Id));
            }
            
            this.DatabaseContext.Add(entity);          
        }

        public void Update(LicenseSpecification entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(LicenseSpecification entity)
        {
            throw new NotImplementedException();
        }

        public LicenseSpecification GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LicenseSpecification> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool IsExist(LicenseSpecification entity)
        {
            throw new NotImplementedException();
        }
    }
}
