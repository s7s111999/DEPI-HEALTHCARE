using System.ComponentModel.DataAnnotations;


namespace authsystem.api.Dtos {
	public class RegisterPatientDto : RegisterDto {
		public string BloodType {get; set;}
		public decimal Height {get; set;}
		public decimal Weight {get; set;}
		public string InsuranceProvider {get; set;}
		public string InsurancePolicyNumber {get; set;}
	}
}
