using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Repositories;

namespace Repository
{
    public static class AssetRepository
    {
        public static IList<Asset> GetAssetsInStock(this IRepository<Asset> repository)
        {
            return repository.Entity.Where(_ => _.AssetStatusID == 1).ToList();
        }

        public static IList<Asset> GetAssetsInUse(this IRepository<Asset> repository)
        {
            return repository.Entity.Where(_ => _.AssetStatusID == 2).ToList();
        }

        public static int GetQuantityOfAsset(this IRepository<Asset> repository, string categoryName)
        {
            return repository.Entity.Count(_ => _.Product.Category.CategoryName == categoryName && _.AssetStatusID == 1);
        }

        public static IList<Asset> GetAssetsByCategoryName(this IRepository<Asset> repository, string categoryName)
        {
            return repository.Entity.Where(_ => _.Product.Category.CategoryName == categoryName &&  _.AssetStatusID == 1 ).ToList();
        }
    }
}