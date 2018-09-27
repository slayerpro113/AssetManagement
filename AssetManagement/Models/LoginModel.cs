using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Models
{
    public class LoginModel
    {
        [Required]
        [RegularExpression(@"(\S)+", ErrorMessage = "White space is not allowed")]
        [ScaffoldColumn(false)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(6)]
        public string PassWord { get; set; }
    }
}