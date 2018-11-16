﻿using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Data.Utilities.Enumeration;
using Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Data.DTO;

namespace Service
{
    public class AssetService : BaseService<Asset>, IAssetService
    {
        private readonly IRepository<Asset> _assetRepository;
        private readonly IRepository<Vendor> _vendorRepository;


        public AssetService(IUnitOfWork unitOfWork, IRepository<Asset> assetRepository, IRepository<Vendor> vendorRepository) : base(unitOfWork, assetRepository)
        {
            _assetRepository = assetRepository;
            _vendorRepository = vendorRepository;
        }

        public IList<Asset> GetAssetsInStock()
        {
            var assets = _assetRepository.GetAssetsInStock();

            foreach (var asset in assets)
            {
                if (asset.AssetStatusID == 1)
                {
                    CalculateDepreciation(asset);
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

                CalculateDepreciation(asset);
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
                asset.CheckinDate = history.CheckinDate.Value.ToShortDateString();
                asset.StaffAssign = history.Employee1.FullName;
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

        public int CalculateTimeLeft(DateTime timeNow, DateTime endTime)
        {
            if (endTime.Year > timeNow.Year)
            {
                return endTime.Month - timeNow.Month + (endTime.Year - timeNow.Year) * 12;
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

        public Asset CalculateDepreciation(Asset asset)
        {
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

            asset.MonthlyAmount = string.Format("{0:0,0 VND}", monthlyAmount);
            asset.DepreciatedAmount = string.Format("{0:0,0 VND}", depreciatedAmount); 
            asset.AmountLeft = string.Format("{0:0,0 VND}", amountLeft); 
            asset.PercentDepreciation = Math.Round((double)percentDepreciation, 2);
            asset.MonthsLeft = monthsLeft;

            return asset;
        }

        public double CalculateVendorPercent(string vendor)
        {
            var assetOfVendor = _assetRepository.CountAssetByVendor(vendor);
            var quantityOfAsset = _assetRepository.CountAsset();

            return ((double)assetOfVendor / (double)quantityOfAsset) * 100;
        }

        public IList<HighChart> GetChartData()
        {
            var vendors = _vendorRepository.GetAll();
            var highCharts = new List<HighChart>();

            foreach (var temp in vendors)
            {
                var highChart = new HighChart();
                highChart.VendorName = temp.Name;
                highChart.PercentVendor = CalculateVendorPercent(temp.Name);

                highCharts.Add(highChart);
            }

            return highCharts;
        }

        public IList<HighChart> Get3DChartData()
        {
            string [] brand = new string[] { "Hoa Phat", "Logitech", "Razer", "SteelSeries", "Dell", "Acer", "Assus", "LG" };
            
            var highCharts = new List<HighChart>();

            for (int i= 0 ; i < brand.Length; i++)
            {
                var highChart = new HighChart();
                highChart.Brand = brand[i];
                highChart.PercentBrand = CalculateBrandPercent(brand[i]);

                highCharts.Add(highChart);
            }

            return highCharts;
        }

        public double CalculateBrandPercent(string brand)
        {
            var assetOfBrand = _assetRepository.CountAssetByBrand(brand);
            var quantityOfAsset = _assetRepository.CountAsset();

            return ((double)assetOfBrand / (double)quantityOfAsset) * 100;
        }
    }
}