using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;

namespace Service
{
    public class CategoryService : BaseService<Category, AssetManagementEntities>, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IRepository<Category> categoryRepository) : base(unitOfWork, categoryRepository)
        {
        }
    }
}