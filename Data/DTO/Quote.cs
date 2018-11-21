namespace Data.Entities
{
    public partial class Quote
    {
        public byte[] ImageBytes { get; set; }
        public string PriceString => string.Format("{0:0,0 VND}", Price);
    }
}