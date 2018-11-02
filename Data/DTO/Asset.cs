using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Asset
    {
        public string AssetStatusName => AssetStatus.StatusName;

        public string AssetName => Product.ProductName;
        public string AssetImage => Product.Image;
        public string AssetBrand => Product.Brand;
        public string AssetCategory => Product.Category.CategoryName;
        public string StaffAssign { get; set; }
        public string CheckinDate { get; set; }

        public int EmployeeId { get; set; }
        public string EmployeeImage { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeAddress { get; set; }
        public string EmployeeJob { get; set; } 
        public string EmployeePhone { get; set; } 
        public string EmployeeBirthdate { get; set; }


        public static IList<Category> GetCategories()
        {
            IList<Category> categories = new List<Category>
            {
                new Category { CategoryName = "Chair", CategoryID = 1 },
                new Category { CategoryName = "Keyboard", CategoryID = 2 },
                new Category { CategoryName = "Mouse", CategoryID = 3 },
                new Category { CategoryName = "PC", CategoryID = 4 },
                new Category { CategoryName = "Printer", CategoryID = 5 },
                new Category { CategoryName = "Screen", CategoryID = 6 }
            };

            return categories;
        }
    }
}