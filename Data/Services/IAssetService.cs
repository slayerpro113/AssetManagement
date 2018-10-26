using System.Collections.Generic;
using Data.Entities;

namespace Data.Services
{
    public interface IAssetService : IBaseService<Asset>
    {
        IList<Asset> GetAssetsInStock();
        IList<Asset> GetAssetsInUse();
        IList<Asset> GetAssetsByHistories(IList<History> histories);
        IList<Asset> GetAvailableAssetsByCategoryName(string categoryName);
    }
}