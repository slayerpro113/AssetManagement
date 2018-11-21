using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Entities
{
    public partial class Order
    {
        public string StaffCreate => Employee.FullName;
        public string Total => string.Format("{0:0,0 VND}", OrderTotal);
        public int NumberOfRequests => PoRequests.Count;
    }
}
