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
            return _assetRepository.GetAssetsInStock();
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
                asset.CheckinDate = history.CheckinDate.Value.ToShortDateString();

                asset.StaffAssign = history.StaffAssign;
                string path = HttpContext.Current.Server.MapPath("~/Image/Categories/" + asset.Product.Image);
                asset.AssetImageByte = File.ReadAllBytes(path);

                assets.Add(asset);
            }
            return assets;
        }
    }
}