using System;

namespace Data.Entities
{
    public partial class Asset
    {
        public string AssetStatusName => AssetStatus.StatusName;

        public string AssetName => Product.ProductName;
        public string AssetImage => Product.Image;
        public string AssetDescription => Product.Description;
        public string AssetBrand => Product.Brand;
        public string AssetCategory => Product.Category.CategoryName;

        public string EmployeeImage { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeAddress { get; set; }
        public string EmployeeJob { get; set; }
        public string EmployeePhone { get; set; }
        public string EmployeeBirthdate { get; set; }

    }
}