using System.ComponentModel.DataAnnotations;

namespace Asp.NetCoreMVCCoding.Models
{
    public class Register
    {
        [Required]
        [StringLength(30, ErrorMessage ="Username can be max 30.")]
        public string Username { get; set; }

        [Required]
        [MinLength(6, ErrorMessage ="Password can be min 6.")]
        [MaxLength(16,ErrorMessage ="Password can be max 16")]
        public string Password { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Repassword can be min 6.")]
        [MaxLength(16, ErrorMessage = "Repassword can be max 16")]
        [Compare(nameof(Password), ErrorMessage ="Password and Repassword can be match.")]
        public string RePassword { get; set; }
    }
}
