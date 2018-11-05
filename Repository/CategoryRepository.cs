using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using Data.Repositories;

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
