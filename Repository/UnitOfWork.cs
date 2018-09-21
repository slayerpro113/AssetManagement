using Data.DataContext;
using Data.Repositories;
using Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDataContext _dataContext;
        private readonly Dictionary<Type, object> _repositories;
        private bool _disposed;
        private ObjectContext _objectContext;
        private DbTransaction _transaction;

        public UnitOfWork(IDataContext dataContext)
        {
            _dataContext = dataContext;
            _repositories = new Dictionary<Type, object>();
        }

        public void BeginTransaction()
        {
            _objectContext = ((IObjectContextAdapter)_dataContext).ObjectContext;
            if (_objectContext.Connection.State != ConnectionState.Open)
            {
                _objectContext.Connection.Open();
            }
            _transaction = _objectContext.Connection.BeginTransaction();
        }

        public bool Commit()
        {
            _dataContext.SaveChanges();
            if (_transaction != null)
            {
                _transaction.Commit();
            }

            return true;
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _dataContext.Dispose();
            _disposed = true;
        }

        public void Dispose()
        {
            if (_objectContext != null && _objectContext.Connection.State == ConnectionState.Open)
                _objectContext.Connection.Close();

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
            }
        }

        public int SaveChanges()
        {
            int i = 0;
            i = _dataContext.SaveChanges();

            return i;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories.Keys.Contains(typeof(TEntity)))
                return (IRepository<TEntity>)_repositories[typeof(TEntity)];

            var repository = new Repository<TEntity>(_dataContext);

            _repositories.Add(typeof(TEntity), repository);

            return (IRepository<TEntity>)_repositories[typeof(TEntity)];
        }
    }
}