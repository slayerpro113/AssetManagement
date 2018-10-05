using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using Repository;

namespace Service
{
    public class AssetService : BaseService<Asset>, IAssetService
    {
        private readonly IRepository<Asset> _assetRepository;

        public AssetService(IUnitOfWork unitOfWork, IRepository<Asset> assetRepository) : base(unitOfWork, assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public IList<Asset> GetAssets()
        {
            var assets = GetAll();

            foreach (Asset asset in assets)
            {
                var history = asset.Histories.FirstOrDefault(_ => _.Checkout_Date == null);
                if (history != null)
                {
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

        public int CountAsset(int productId)
        {
            return _assetRepository.CountAsset(productId);
        }
    }
}