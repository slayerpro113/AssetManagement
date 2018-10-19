using Data.Entities;
using Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public static class HistoryRepository
    {
        public static IList<History> GetHistoriesByAssetId(this IRepository<History> repository, int assetId)
        {
            var histories = repository.Entity.Where(_ => _.AssetID == assetId).OrderByDescending(_ => _.CheckinDate).ToList();
            return histories;
        }

        public static History GetHistoryByAssetId(this IRepository<History> repository, int assetId)
        {
            var history = repository.Entity.FirstOrDefault(_ => _.AssetID == assetId && _.CheckoutDate == null);
            return history;
        }

        public static IList<History> GetHistoriesByEmployeeId(this IRepository<History> repository, int employeeId)
        {
            var histories = repository.Entity.Where(_ => _.EmployeeID == employeeId && _.CheckoutDate == null).ToList();
            return histories;
        }
    }
}