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

        public static int CountProductByName(this IRepository<Product> repository, string productName)
        {
            var count = repository.Entity.Where(_ => _.ProductName == productName).ToList().Count;
            return count;
        }

        public static Product GetProductsByProductName(this IRepository<Product> repository, string productName)
        {
            var product = repository.Entity.FirstOrDefault(_ => _.ProductName == productName);
            return product;
        }
    }
}