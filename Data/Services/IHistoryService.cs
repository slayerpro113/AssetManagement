using System.Collections.Generic;
using Data.Entities;
using Data.Utilities.Enumeration;

namespace Data.Services
{
    public interface IHistoryService : IBaseService<History>
    {
        IList<History> GetHistoriesByAssetId(int assetId);
        Enumerations.AddEntityStatus HandleAssign(Employee employee, Asset asset, string assignRemark, string staffAssign);
        Enumerations.UpdateEntityStatus HandleRecall(int assetId, string recallRemark, string staffRecall);
        IList<History> GetHistoriesByEmployeeId(int employeeId);
    }
}