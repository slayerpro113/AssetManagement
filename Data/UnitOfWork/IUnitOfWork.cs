using Data.Repositories;
using System;

namespace Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();

        void Dispose(bool disposing);

        IRepository<TEntity> Repository<TEntity>() where TEntity : class;

        void BeginTransaction();

        bool Commit();

        void Rollback();
    }
}