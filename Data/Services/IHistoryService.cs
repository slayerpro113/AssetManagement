using System.Collections.Generic;
using Data.Entities;
using Data.Utilities.Enumeration;

namespace Data.Services
{
    public interface IHistoryService : IBaseService<History>
    {
        IList<History> GetHistoriesByAssetId(int assetId);
        Enumerations.AddEntityStatus HandleHistory(int assetId, int employeeId);
        Enumerations.UpdateEntityStatus SaveCheckoutDate(int assetId, string remark);
    }
}