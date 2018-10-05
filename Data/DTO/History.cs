namespace Data.Entities
{
    public partial class History
    {
        public string EmployeeName => Employee.FullName;
    }
}