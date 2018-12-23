using Data.DTO;
using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using Data.Utilities.Enumeration;
using Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Service
{
    public class AssetService : BaseService<Asset>, IAssetService
    {
        private readonly IRepository<Asset> _assetRepository;
        private readonly IRepository<Vendor> _vendorRepository;
        private readonly IRepository<Product> _productRepository;

        public AssetService(IUnitOfWork unitOfWork, IRepository<Asset> assetRepository, IRepository<Vendor> vendorRepository, IRepository<Product> productRepository) : base(unitOfWork, assetRepository)
        {
            _assetRepository = assetRepository;
            _vendorRepository = vendorRepository;
            _productRepository = productRepository;
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

        public bool IsExistedBarcode(string barcode)
        {
            var asset = _assetRepository.GetAssetByBarcode(barcode);
            if (asset != null)
            {
                return true;
            }
            else return false;
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

            foreach (var vendor in vendors)
            {
                var highChart = new HighChart
                {
                    VendorName = vendor.Name,
                    PercentVendor = CalculateVendorPercent(vendor.Name)
                };

                highCharts.Add(highChart);
            }

            return highCharts;
        }

        public IList<HighChart> Get3DChartData()
        {
            var products = _productRepository.GetBrands();

            var highCharts = new List<HighChart>();

            foreach (var product in products)
            {
                var highChart = new HighChart
                {
                    Brand = product.Brand,
                    PercentBrand = CalculateBrandPercent(product.Brand)
                };

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

        public string GenerateBarCode()
        {
            Random rd = new Random();
            int rand = rd.Next(99999) + 100000;
            string barcode = "EL" + rand;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (Bitmap bitMap = new Bitmap(barcode.Length * 21, 65))
                {
                    using (Graphics graphics = Graphics.FromImage(bitMap))
                    {
                        Font oFont = new Font("IDAutomationHC39M", 12);
                        PointF point = new PointF(15f, 4f);
                        SolidBrush whiteBrush = new SolidBrush(Color.Black);
                        graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                        SolidBrush blackBrush = new SolidBrush(Color.White);
                        graphics.DrawString(barcode, oFont, blackBrush, point);
                    }

                    bitMap.Save(memoryStream, ImageFormat.Jpeg);

                    var barcodeSrc = "data:image/png;base64," + Convert.ToBase64String(memoryStream.ToArray());
                    return barcode + "|" + barcodeSrc;
                }
            }
        }
    }
}