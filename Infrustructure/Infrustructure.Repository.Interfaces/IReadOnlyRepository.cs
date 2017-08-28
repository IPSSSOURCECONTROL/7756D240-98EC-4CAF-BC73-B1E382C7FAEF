using System.Collections.Generic;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.Repository.Interfaces
{
    public interface IReadOnlyRepository<TEntity> where TEntity:class 
    {
        TEntity GetById(string id);
        IEnumerable<TEntity> GetAll();
        bool IsExist(TEntity entity);
    }
}
