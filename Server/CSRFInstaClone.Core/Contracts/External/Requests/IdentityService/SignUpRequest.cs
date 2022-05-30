using System.ComponentModel.DataAnnotations;

namespace CSRFInstaClone.Core.Contracts.External.Requests.IdentityService; 

public class SignUpRequest {
	[Required]
	public string Username { get; set; }
	
	[Required, EmailAddress]
	public string Email { get; set; }
	
	[Required]
	public string Password { get; set; }
}