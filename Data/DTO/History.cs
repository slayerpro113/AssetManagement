using System;

namespace Data.Entities
{
    public partial class History
    {
        public string EmployeeName => Employee.FullName;

        public string StaffAssign => Employee1.FullName;
    }
}