using System.ComponentModel.DataAnnotations;

namespace AssetManagement.Models
{
    public class LoginModel
    {
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(20)]
        [MinLength(6)]
        public string PassWord { get; set; }
    }
}