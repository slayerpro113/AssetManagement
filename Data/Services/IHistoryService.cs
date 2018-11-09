using System.Collections.Generic;
using Data.Entities;
using Data.Utilities.Enumeration;

namespace Data.Services
{
    public interface IHistoryService : IBaseService<History>
    {
        IList<History> GetHistoriesByAssetId(int assetId);
        Enumerations.AddEntityStatus HandleAssign(int poRequestId, Employee employee, Asset asset, string staffAssign);
        Enumerations.AddEntityStatus HandleAssignWithoutRequest(Employee employee, Asset asset, string staffAssign);
        Enumerations.UpdateEntityStatus HandleRecall(int assetId, string staffRecall);
        IList<History> GetHistoriesByEmployeeId(int employeeId);
    }
}