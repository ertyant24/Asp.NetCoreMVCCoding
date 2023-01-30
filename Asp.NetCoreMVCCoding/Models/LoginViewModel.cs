using System.ComponentModel.DataAnnotations;

namespace Asp.NetCoreMVCCoding.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is Required.")]
        [StringLength(30, ErrorMessage = "Username can be max 30.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [MinLength(6, ErrorMessage = "Password can be min 6.")]
        [MaxLength(16, ErrorMessage = "Password can be max 16.")]
        public string Password { get; set; }
    }
}
