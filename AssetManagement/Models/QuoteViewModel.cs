using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AssetManagement.Models
{
    public class QuoteViewModel
    {
        public int QuoteID { get; set; }
        public string Image { get; set; }
        [Required]

        [MaxLength(25)]
        public string ProductName { get; set; }
        [MaxLength(20)]

        public string Brand { get; set; }

        public string CategoryName { get; set; }
        [Required]

        [MaxLength(20)]
        public string Vendor { get; set; }

        [Required]
        [MaxLength(10)]
        public decimal Price { get; set; }

        public string PriceString { get; set; }

        [MaxLength(3)]
        public Nullable<int> Warranty { get; set; }

        [Required]
        public byte[] ImageBytes { get; set; }

    }
}