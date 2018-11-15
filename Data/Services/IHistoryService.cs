using System.Collections.Generic;
using Data.Entities;
using Data.Utilities.Enumeration;

namespace Data.Services
{
    public interface IHistoryService : IBaseService<History>
    {
        IList<History> GetHistoriesByAssetId(int assetId);
        Enumerations.AddEntityStatus HandleAssign(int poRequestId, Employee employee, Asset asset, int staffAssignId);
        Enumerations.AddEntityStatus HandleAssignWithoutRequest(Employee employee, Asset asset, int staffAssignId);
        Enumerations.UpdateEntityStatus HandleRecall(int assetId, int staffRecallId);
        IList<History> GetHistoriesByEmployeeId(int employeeId);
    }
}