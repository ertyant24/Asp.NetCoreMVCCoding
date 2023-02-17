using System.ComponentModel.DataAnnotations;

namespace Asp.NetCoreMVCCoding.Models
{
	public class EditViewModel
	{
		public int Id { get; set; }

		[Required]
		[StringLength(30, ErrorMessage = "Username can be max 30.")]
		public string Username { get; set; }

		[Required]
		[StringLength(50)]
		public string FullName { get; set; }
		public bool Locked { get; set; }

		[Required]
		[StringLength(50)]
		public string Role { get; set; }

	}
}
