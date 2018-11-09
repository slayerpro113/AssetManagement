using System;
using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Repository;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Data.Utilities.Enumeration;

namespace Service
{
    public class AssetService : BaseService<Asset>, IAssetService
    {
        private readonly IRepository<Asset> _assetRepository;

        public AssetService(IUnitOfWork unitOfWork, IRepository<Asset> assetRepository) : base(unitOfWork, assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public IList<Asset> GetAssetsInStock()
        {
            var assets = _assetRepository.GetAssetsInStock();

            foreach (var asset in assets)
            {
                if (asset.AssetStatusID == 2)
                {
                    //tien khau hao hang thang = tong tien / tong thang
                    var monthlyAmount = asset.OrderDetail.Price / asset.MonthDepreciation;
                    //So thang da khau hao = t/g hien tai - t/g mua
                    var depreciatedMonth = CalculateTimeLeft(asset.OrderDetail.Order.PurchaseDate, DateTime.Now);

                    //so tien da khau hao = so thang da khau hao * Tien khau hao 1 thang
                    var depreciatedAmount = depreciatedMonth * monthlyAmount;
                    //so tien con lai = tien mua - tien da khau hao
                    var amountLeft = asset.OrderDetail.Price - depreciatedAmount;

                    var percentDepreciation = depreciatedAmount % asset.OrderDetail.Price * 100;

                    var monthsLeft = CalculateTimeLeft(DateTime.Now, asset.EndTimeDepreciation.Value);


                    asset.MonthlyAmount = monthlyAmount;
                    asset.DepreciatedAmount = depreciatedAmount;
                    asset.AmountLeft = amountLeft;
                    asset.PercentDepreciation = percentDepreciation;
                    asset.MonthsLeft = monthsLeft;
                }
            }

            return assets;
        }

        public IList<Asset> GetAssetsInUse()
        {
            var assets = _assetRepository.GetAssetsInUse();

            foreach (var asset in assets)
            {
                //get current owner
                var history = asset.Histories.FirstOrDefault(_ => _.CheckoutDate == null);
                if (history != null)
                {
                    asset.EmployeeId = history.Employee.EmployeeID;
                    asset.EmployeeImage = history.Employee.Image;
                    asset.EmployeeName = history.Employee.FullName;                   
                    asset.EmployeeEmail = history.Employee.Email;  
                }

                //tien khau hao hang thang = tong tien / tong thang
                var monthlyAmount = asset.OrderDetail.Price / asset.MonthDepreciation;

                //So thang da khau hao = t/g hien tai - t/g mua
                var depreciatedMonth = CalculateTimeLeft(asset.OrderDetail.Order.PurchaseDate, DateTime.Now);

                //so tien da khau hao = so thang da khau hao * Tien khau hao 1 thang
                var depreciatedAmount = depreciatedMonth * monthlyAmount;
                
                //so tien con lai = tien mua - tien da khau hao
                var amountLeft = asset.OrderDetail.Price - depreciatedAmount;


                var percentDepreciation = depreciatedAmount / asset.OrderDetail.Price * 100;

                var monthsLeft = CalculateTimeLeft(DateTime.Now, asset.EndTimeDepreciation.Value);


                asset.MonthlyAmount = monthlyAmount;
                asset.DepreciatedAmount = depreciatedAmount;
                asset.AmountLeft = amountLeft;
                asset.PercentDepreciation = percentDepreciation;
                asset.MonthsLeft = monthsLeft;
            }
            return assets;
        }

        public IList<Asset> GetAssetsByHistories(IList<History> histories)
        {
            IList<Asset> assets = new List<Asset>();
            foreach (var history in histories)
            {
                var asset = new Asset();
                asset = history.Asset;
                asset.Product = history.Asset.Product;
                asset.CheckinDate = history.CheckinDate.Value.ToLongDateString();
                asset.StaffAssign = history.StaffAssign;
                assets.Add(asset);
            }
            return assets;
        }

        public IList<Asset> GetAvailableAssetsByCategoryName(string categoryName)
        {
            return _assetRepository.GetAssetsByCategoryName(categoryName);
        }

        public Enumerations.UpdateEntityStatus HandleEnterDetail(int assetId, string barcode, int monthsOfDepreciation)
        {
            try
            {
                var asset = GetEntity(assetId);
                asset.Barcode = barcode;
                asset.MonthDepreciation = monthsOfDepreciation;
                asset.AssetStatusID = 1;

                var purchaseDate = asset.OrderDetail.Order.PurchaseDate;
                var endTimeDepreciation = purchaseDate.AddMonths(monthsOfDepreciation);
                asset.EndTimeDepreciation = endTimeDepreciation;

                UpdateEntity(asset);
                return Enumerations.UpdateEntityStatus.Success;
            }
            catch (Exception e)
            {
                return Enumerations.UpdateEntityStatus.Failed;
            }
        }

        public Asset GetAssetDepreciationDetail(int assetId)
        {
            throw new NotImplementedException();
        }

        public int CalculateTimeLeft(DateTime timeNow, DateTime endTime)
        {
            if (endTime.Year > timeNow.Year)
            {
                return  endTime.Month - timeNow.Month + (endTime.Year - timeNow.Year) * 12;
            }
            else if (endTime.Year == timeNow.Year)
            {
                return endTime.Month - timeNow.Month;
            }
            else
            {
                return 0;
            }
        }
    }
}