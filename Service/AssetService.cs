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
                    asset.EmployeeId = history.PoRequest.Employee.EmployeeID;
                    asset.EmployeeImage = history.PoRequest.Employee.Image;
                    asset.EmployeeName = history.PoRequest.Employee.FullName;
                    asset.EmployeeAddress = history.PoRequest.Employee.Address;
                    asset.EmployeeEmail = history.PoRequest.Employee.Email;
                    asset.EmployeeJob = history.PoRequest.Employee.JobTitle;
                    asset.EmployeePhone = history.PoRequest.Employee.Phone;
                    asset.EmployeeBirthdate = history.PoRequest.Employee.BirthDate;
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
                assets.Add(asset);
            }
            return assets;
        }

        public IList<Asset> GetAvailableAssetsByCategoryName(string categoryName)
        {
            return _assetRepository.GetAssetsByCategoryName(categoryName);
        }
    }
}