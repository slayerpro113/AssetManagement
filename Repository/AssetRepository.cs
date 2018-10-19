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
    }
}