using System.ComponentModel.DataAnnotations;

namespace IdentityService.Core.V1.Contracts.Requests.Authentication; 

public class LoginRequest {
	[Required]
	public string UsernameEmail { get; set; }
	
	[Required]
	public string Password { get; set; }
}