using Data.DataContext;
using Data.Entities;
using Data.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AssetManagementEntities _context;
        protected readonly DbSet<TEntity> _dbSet;
        public DbSet<TEntity> Entity => _dbSet;

        public IDataContext Context => _context;

        public Repository(IDataContext context)
        {
            _context = (context as AssetManagementEntities);
            _dbSet = _context.Set<TEntity>();
        }

        public void AddEntity(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public IList<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity GetEntity(object id)
        {
            return _dbSet.Find(id);
        }

        public void UpdateEntity(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteEntity(object id)
        {
            _dbSet.Remove(_dbSet.Find(id));
        }
    }
}