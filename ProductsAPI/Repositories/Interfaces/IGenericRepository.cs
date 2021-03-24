using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IList<TEntity> GetAll();
        TEntity GetById(object id);
        object Insert(TEntity entity, bool saveChanges = false);
        void Delete(object id, bool saveChanges = false);
        void Update(TEntity entity, bool saveChanges = false);
        void Commit();
        void Dispose();
    }
}
