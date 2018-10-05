using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Models
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }

        [Required]
        [MaxLength(20)]
        public string ProductName { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(20)]
        public string Brand { get; set; }

        public int CategoryID { get; set; }
        public string Image { get; set; }

        [Required]
        public byte[] ImageBytes { get; set; }
    }
}