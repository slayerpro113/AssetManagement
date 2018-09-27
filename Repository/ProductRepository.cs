using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Repositories;

namespace Repository
{
    public static class ProductRepository
    {
        public static IList<Product> GetProductsByCategoryId(this IRepository<Product> repository, int categoryId)
        {
            var products = repository.Entity.Where(_ => _.CategoryID == categoryId).ToList();
            return products;
        }
    }
}