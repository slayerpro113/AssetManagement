using Data.Entities;
using Data.Repositories;
using System.Linq;

namespace Repository
{
    public static class CategoryRepository
    {
        public static Category GetCategoryByCategoryName(this IRepository<Category> repository, string categoryName)
        {
            return repository.Entity.FirstOrDefault(_ => _.CategoryName == categoryName);
        }
    }
}