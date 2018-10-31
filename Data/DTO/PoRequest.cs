
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class PoRequest
    {
        public int Quantity { get; set; }
        public string EmployeeName => Employee.FullName;
        public string EmployeeImage => Employee.Image;
        public string Status => RequestStatus.StatusName;
        public string AssetImage { get; set; }
        public string StaffAssign { get; set; }
        public string AssetName { get; set; }

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


        public static IList<RequestStatus> GetStatuses()
        {
            IList<RequestStatus> statuses = new List<RequestStatus>
            {
                new RequestStatus { StatusName = "In Progress", RequestStatusID = 1 },
                new RequestStatus { StatusName = "Wait For Approving", RequestStatusID = 2 },
                new RequestStatus { StatusName = "Approved", RequestStatusID = 3 },
                new RequestStatus { StatusName = "Finish", RequestStatusID = 4 },
            };

            return statuses;
        }
    }
}