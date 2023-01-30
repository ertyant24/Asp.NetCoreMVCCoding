using System.ComponentModel.DataAnnotations;

namespace Asp.NetCoreMVCCoding.Datas
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)] 
        public string? FullName { get; set; }

        [Required]
        [StringLength(30)]  
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }
        public bool Locked { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
