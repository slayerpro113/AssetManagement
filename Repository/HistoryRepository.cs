using System;
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
            return repository.Entity.Where(_ => _.AssetID == assetId).OrderByDescending(_ => _.CheckinDate).ToList();
        }

        public static History GetHistoryByAssetId(this IRepository<History> repository, int assetId)
        {
            return repository.Entity.FirstOrDefault(_ => _.AssetID == assetId && _.CheckoutDate == null);
        }

        public static IList<History> GetHistoriesByEmployeeId(this IRepository<History> repository, int employeeId)
        {
            return repository.Entity.Where(_ => _.EmployeeID == employeeId && _.CheckoutDate == null).ToList();
        }

        //Call from PoRequestservice, get PoRequest history
        public static History GetStaffAssignAndAssetImage(this IRepository<History> repository, PoRequest poRequest)
        {
            return repository.Entity.FirstOrDefault(_ =>_.EmployeeID == poRequest.EmployeeID && _.Asset.Product.Category.CategoryName == poRequest.CategoryName && _.CheckinDate == poRequest.FinishedDate);
        }
    }
}