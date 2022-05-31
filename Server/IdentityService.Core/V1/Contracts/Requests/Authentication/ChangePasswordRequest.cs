using System.ComponentModel.DataAnnotations;

namespace IdentityService.Core.V1.Contracts.Requests.Authentication; 

public class ChangePasswordRequest {
	[Required, EmailAddress]
	public string Email { get; set; }
	
	[Required]
	public string ResetToken { get; set; }
	
	[Required]
	public string NewPassword { get; set; }
}