using Data.DataContext;
using System.Collections.Generic;
using System.Data.Entity;

namespace Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IDataContext context { get; }
        DbSet<TEntity> Entity { get; }

        IList<TEntity> GetAll();

        void AddEntity(TEntity entity);
    }
}