using System.Collections.Generic;
using Data.Entities;

namespace Data.Services
{
    public interface IAssetService : IBaseService<Asset>
    {
        IList<Asset> GetAssets();
        void SetAssetStatus(int assetId, int statusId);
    }
}