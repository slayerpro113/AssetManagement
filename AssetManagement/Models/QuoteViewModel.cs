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
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string CategoryName { get; set; }
        [Required]
        public string Vendor { get; set; }
        [Required]
        public decimal Price { get; set; }
        public Nullable<int> Warranty { get; set; }

        [Required]
        public byte[] ImageBytes { get; set; }

    }
}