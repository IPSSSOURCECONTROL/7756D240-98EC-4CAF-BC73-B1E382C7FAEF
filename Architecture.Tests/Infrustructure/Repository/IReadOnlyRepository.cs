using System.Collections.Generic;

namespace Architecture.Tests.Infrustructure.Repository
{
    public interface IReadOnlyRepository<TEntity> where TEntity:class 
    {
        TEntity GetById(string id);
        IEnumerable<TEntity> GetAll();
        bool IsExist(TEntity entity);
    }
}
