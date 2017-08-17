using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KhanyisaIntel.Kbit.Framework.Infrustructure.MongoDb;
using KhanyisaIntel.Kbit.Framework.Infrustructure.Repository;
using KhanyisaIntel.Kbit.Framework.Security.Domain.AspNet;
using KhanyisaIntel.Kbit.Framework.Security.Repository.Interfaces;

namespace KhanyisaIntel.Kbit.Framework.Security.Repository
{
    public class AspNetPrincipleUserRepository: BasicRepositoryBase, IAspNetPrincipleUserRepository
    {
        public AspNetPrincipleUserRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public void Add(AspNetPrincipleUser entity)
        {
            throw new NotImplementedException();
        }

        public void Update(AspNetPrincipleUser entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(AspNetPrincipleUser entity)
        {
            throw new NotImplementedException();
        }

        public AspNetPrincipleUser GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AspNetPrincipleUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool IsExist(AspNetPrincipleUser entity)
        {
            throw new NotImplementedException();
        }
    }
}
