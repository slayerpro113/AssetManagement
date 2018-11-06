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
                if (asset.AssetID == 60)
                {
                    //tien khau hao hang thang = tong tien / tong thang
                    var monthlyAmount = asset.OrderDetail.Price / asset.MonthDepreciation;
                    //So thang da khau hao = t/g hien tai - t/g mua
                    var depreciatedMonth = DateTime.Now.Month - asset.OrderDetail.Order.PurchaseDate.Month;

                    //so tien da khau hao = so thang da khau hao * Tien khau hao 1 thang
                    var depreciatedAmount = depreciatedMonth * monthlyAmount;
                    //so tien con lai = tien mua - tien da khau hao
                    var amountLeft = asset.OrderDetail.Price - depreciatedAmount;

                    var percentDepreciation = depreciatedAmount % asset.OrderDetail.Price * 100;

                    var monthsLeft = asset.EndTimeDepreciation.Value.AddMonths(-DateTime.Now.Month).Month ;


                    asset.monthlyAmount = monthlyAmount;
                    asset.depreciatedAmount = depreciatedAmount;
                    asset.amountLeft = amountLeft;
                    asset.percentDepreciation = percentDepreciation;
                    asset.monthsLeft = monthsLeft;
                    asset.monthsLeft = monthsLeft;
                }
            }

            return assets;
        }

        public IList<Asset> GetAssetsInUse()
        {
            var assets = _assetRepository.GetAssetsInUse();

            foreach (var asset in assets)
            {
                var history = asset.Histories.FirstOrDefault(_ => _.CheckoutDate == null);
                if (history != null)
                {
                    asset.EmployeeId = history.Employee.EmployeeID;
                    asset.EmployeeImage = history.Employee.Image;
                    asset.EmployeeName = history.Employee.FullName;
                    asset.EmployeeAddress = history.Employee.Address;
                    asset.EmployeeEmail = history.Employee.Email;
                    asset.EmployeeJob = history.Employee.JobTitle;
                    asset.EmployeePhone = history.Employee.Phone;
                    asset.EmployeeBirthdate = history.Employee.BirthDate;
                }
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
    }
}