using System.ComponentModel.DataAnnotations;


namespace authsystem.api.Dtos {
	public class RegisterDoctorDto : RegisterDto {
		public string Specialization {get; set;}
		public string LicenseNumber {get; set;}
		public string Qualificatoins {get; set;}
		[Range(1, 50, ErrorMessage = "Experience must be 1-50 years")]
		public int YearsOfExperience {get; set;}
	}
}