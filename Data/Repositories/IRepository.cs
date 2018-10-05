using Data.DataContext;
using System.Collections.Generic;
using System.Data.Entity;

namespace Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> Entity { get; }

        IList<TEntity> GetAll();

        void AddEntity(TEntity entity);

        TEntity GetEntity(object id);

        void UpdateEntity(TEntity entity);

        void DeleteEntity(object id);
    }
}