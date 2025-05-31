using authsystem.api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using hossystem.core.Models;

namespace authsystem.api.Controllers{

	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IConfiguration _config;
	
		public AuthController(UserManager<ApplicationUser> userManager, IConfiguration config)
		{
			_userManager = userManager;
			_config = config;
		}
	
	
		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDto model)
		{
			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
				return Unauthorized();
	
			var token = GenerateJwtToken(user);
			return Ok(new { Token = token });
		}
	
		private string GenerateJwtToken(ApplicationUser user)
		{
			var claims = new[]
			{
				new Claim("email", user.Email),
				new Claim("uid", user.Id)
			};
	
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
	
			var token = new JwtSecurityToken(
				issuer: _config["JwtSettings:Issuer"],
				audience: _config["JwtSettings:Audience"],
				claims: claims,
				expires: DateTime.Now.AddDays(1),
				signingCredentials: creds);
	
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
		
		[HttpPost("register-doc")]
		public async Task<IActionResult> RegisterDoctor(RegisterDoctorDto model){
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState); 
			}
			
			var existingUser = await _userManager.FindByEmailAsync(model.Email);
			if (existingUser != null)
			{
				return BadRequest(new
				{
					Success = false,
					Message = "Email address is already registered",
					Errors = new[] { "The email address is already in use" }
				});
			}
			
			
			// Create doctor user
			var doctor = new Doctor
			{
				UserName = model.Email,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Specialization = model.Specialization,
				LicenseNumber = model.LicenseNumber,
				YearsOfExperience = model.YearsOfExperience,
				EmailConfirmed = true,
				Address = model.Address
			};
	
			// Create the doctor account
			var result = await _userManager.CreateAsync(doctor, model.Password);
	
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
				return BadRequest(ModelState);
			}
	
			// Assign Doctor role
			await _userManager.AddToRoleAsync(doctor, "Doctor");
	
			return Ok(new 
			{ 
				Success = true,
				Message = "Doctor registration successful",
				DoctorId = doctor.Id,
				Email = doctor.Email
			});
		}
		
		[HttpPost("register-pat")]
		public async Task<IActionResult> RegisterPatient(RegisterPatientDto model){
			if (!ModelState.IsValid) 
			{
				return BadRequest(ModelState); 
			}
			
			var existingUser = await _userManager.FindByEmailAsync(model.Email);
			if (existingUser != null)
			{
				return BadRequest(new
				{
					Success = false,
					Message = "Email address is already registered",
					Errors = new[] { "The email address is already in use" }
				});
			}
			
			
			// Create doctor user
			var doctor = new Patient
			{
				UserName = model.Email,
				Email = model.Email,
				FirstName = model.FirstName,
				LastName = model.LastName,
				BloodType = model.BloodType,
				Height = model.Height,
				Weight = model.Weight,
				EmailConfirmed = true,
				InsuranceProvider = model.InsuranceProvider,
				InsurancePolicyNumber = model.InsurancePolicyNumber,
				Address = model.Address,
				DateOfBirth = model.DateOfBirth
			};
	
			// Create the doctor account
			var result = await _userManager.CreateAsync(doctor, model.Password);
	
			if (!result.Succeeded)
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
				return BadRequest(ModelState);
			}
	
			// Assign Doctor role
			await _userManager.AddToRoleAsync(doctor, "Patient");
	
			return Ok(new 
			{ 
				Success = true,
				Message = "Patient registration successful",
				DoctorId = doctor.Id,
				Email = doctor.Email
			});
		}
	}
}
