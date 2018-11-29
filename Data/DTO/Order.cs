namespace Data.Entities
{
    public partial class Order
    {
        public string StaffCreate => Employee.FullName;
        public string Total => string.Format("{0:0,0 VND}", OrderTotal);
        public int NumberOfRequests => PoRequests.Count;
    }
}