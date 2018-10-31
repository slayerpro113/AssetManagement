namespace Data.Entities
{
    public partial class Product
    {
        public byte[] ImageBytes { get; set; }
        public string ProductType => Category.CategoryName;
        public int NumberUsingProduct { get; set; }
    }
}