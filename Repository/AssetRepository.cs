using System.Linq;
using Data.Entities;
using Data.Repositories;

namespace Repository
{
    public static class AssetRepository
    {
        public static int CountAsset(this IRepository<Asset> repository,int productId)
        {
            var count = repository.Entity.Count(_ => _.ProductID == productId);
            return count;
        }
    }
}