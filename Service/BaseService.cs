using Data.DataContext;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using System.Collections.Generic;

namespace Service
{
    public abstract class BaseService<TEntity, TContext> : IBaseService<TEntity> where TEntity : class where TContext : IDataContext
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        protected BaseService(IUnitOfWork unitOfWork, IRepository<TEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public void AddEntity(TEntity entity)
        {
            _repository.AddEntity(entity);
            _unitOfWork.SaveChanges();
        }

        public TEntity Get(object id)
        {
            return _repository.Get(id);
        }

        public IList<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public void UpdateEntity(TEntity entity)
        {
            _repository.UpdateEntity(entity);
            _unitOfWork.SaveChanges();
        }
    }
}