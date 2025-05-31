using System.ComponentModel.DataAnnotations;

namespace authsystem.api.Dtos {
	public class RegisterDto {
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress(ErrorMessage = "Invalid email format")]
		public string Email { get; set; }
		
		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Passwords do not match")]
		public string ConfirmPassword { get; set; }
		public string Address {get; set;}
		public DateTime DateOfBirth {get; set;}
		public string FirstName {get; set;}
		public string LastName {get; set;}
	}
}
