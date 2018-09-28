using System.Collections.Generic;

namespace Data.Services
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        IList<TEntity> GetAll();

        void AddEntity(TEntity entity);
        TEntity Get(object id);

        void UpdateEntity(TEntity entity);
    }
}