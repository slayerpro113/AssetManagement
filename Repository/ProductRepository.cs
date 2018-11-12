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
            return repository.Entity.Where(_ => _.CategoryID == categoryId).ToList();
        }

        public static int CountProductByName(this IRepository<Product> repository, string productName)
        {
            return repository.Entity.Where(_ => _.ProductName == productName).ToList().Count;
        }

        public static Product GetProductsByProductName(this IRepository<Product> repository, string productName)
        {
            return repository.Entity.FirstOrDefault(_ => _.ProductName == productName);
        }
    }
}