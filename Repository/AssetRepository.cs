using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Repositories;

namespace Repository
{
    public static class AssetRepository
    {
        public static IList<Asset> GetAssetsInStock(this IRepository<Asset> repository)
        {
            return repository.Entity.Where(_ => _.AssetStatusID != 2).OrderByDescending(_ => _.AssetID).ToList();
        }

        public static IList<Asset> GetAssetsInUse(this IRepository<Asset> repository)
        {
            return repository.Entity.Where(_ => _.AssetStatusID == 2).OrderByDescending(_ => _.AssetID).ToList();
        }

        public static int GetQuantityOfAsset(this IRepository<Asset> repository, string categoryName)
        {
            return repository.Entity.Count(_ => _.Product.Category.CategoryName == categoryName && _.AssetStatusID == 1);
        }

        public static IList<Asset> GetAssetsByCategoryName(this IRepository<Asset> repository, string categoryName)
        {
            return repository.Entity.Where(_ => _.Product.Category.CategoryName == categoryName &&  _.AssetStatusID == 1 ).ToList();
        }

        public static int CountAssetByVendor(this IRepository<Asset> repository, string vendor)
        {
            return repository.Entity.Where(_ => _.OrderDetail.Vendor.Name == vendor).ToList().Count;
        }

        public static int CountAssetByBrand(this IRepository<Asset> repository, string brand)
        {
            return repository.Entity.Where(_ => _.Product.Brand == brand).ToList().Count;
        }

        public static int CountAsset(this IRepository<Asset> repository)
        {
            return repository.Entity.Count();
        }

        public static Asset GetAssetByBarcode(this IRepository<Asset> repository, string barcode)
        {
            return repository.Entity.FirstOrDefault(_ => _.Barcode == barcode);
        }
    }
}