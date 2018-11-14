using System.Collections.Generic;
using Data.DTO;
using Data.Entities;
using Data.Utilities.Enumeration;

namespace Data.Services
{
    public interface IAssetService : IBaseService<Asset>
    {
        IList<Asset> GetAssetsInStock();
        IList<Asset> GetAssetsInUse();
        IList<Asset> GetAssetsByHistories(IList<History> histories);
        IList<Asset> GetAvailableAssetsByCategoryName(string categoryName);
        Enumerations.UpdateEntityStatus HandleEnterDetail(int assetId, string barcode, int monthsOfDepreciation);
        IList<HighChart> GetChartData();
        IList<HighChart> Get3DChartData();
    }
}