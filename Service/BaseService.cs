using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using System.Collections.Generic;

namespace Service
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
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

        public TEntity GetEntity(object id)
        {
            return _repository.GetEntity(id);
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

        public void DeleteEntity(object id)
        {
            _repository.DeleteEntity(id);
            _unitOfWork.SaveChanges();
        }
    }
}