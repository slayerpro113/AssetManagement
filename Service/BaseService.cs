using Data.DataContext;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public abstract class BaseService<TEntity, TContext> : IBaseService<TEntity> where TEntity : class where TContext : IDataContext
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;

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

        public IList<TEntity> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
