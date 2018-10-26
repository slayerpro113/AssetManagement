namespace Data.Entities
{
    public partial class History
    {
        public string EmployeeName => PoRequest.Employee.FullName;
    }
}