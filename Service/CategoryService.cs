using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;

namespace Service
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        public CategoryService(IUnitOfWork unitOfWork, IRepository<Category> categoryRepository) : base(unitOfWork, categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
    }
}