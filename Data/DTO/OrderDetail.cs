using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public partial class OrderDetail
    {
        public string ProductName => Quote.ProductName;
        public string ProductImage => Quote.Image;
        public string VendorName => Vendor.Name;


        public static IList<Vendor> GetVendors()
        {
            IList<Vendor> vendors = new List<Vendor>
            {
                new Vendor { Name = "Hoa Phat", VendorID = 1 },
                new Vendor { Name = "Phi Long", VendorID = 2  },
                new Vendor { Name = "Phong Vu", VendorID = 3 },
                new Vendor { Name = "Xuan Vinh", VendorID = 4 }
               
            };

            return vendors;
        }
    }
}
