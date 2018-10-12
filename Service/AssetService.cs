using System;
using Data.Entities;
using Data.Repositories;
using Data.Services;
using Data.UnitOfWork;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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

        public IList<Asset> GetAssets()
        {
            var assets = GetAll();

            foreach (Asset asset in assets)
            {
                string path = HttpContext.Current.Server.MapPath("~/Image/Categories/" + asset.AssetImage);
                asset.AssetImageBytes = File.ReadAllBytes(path);

                var history = asset.Histories.FirstOrDefault(_ => _.Checkout_Date == null);
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
                    string path2 = HttpContext.Current.Server.MapPath("~/Image/" + asset.EmployeeImage);
                    asset.EmployeeImageBytes = File.ReadAllBytes(path2);
                }
            }
            return assets;
        }

        public void SetAssetStatus(int assetId, int assetStatus)
        {
            var asset = GetEntity(assetId);
            asset.AssetStatusID = assetStatus;
            UpdateEntity(asset);
        }
    }
}