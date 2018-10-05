using System.Collections.Generic;
using Data.Entities;

namespace Data.Services
{
    public interface IAssetService : IBaseService<Asset>
    {
        IList<Asset> GetAssets();
        int CountAsset(int productId);
    }
}