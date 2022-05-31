using System.ComponentModel.DataAnnotations;

namespace IdentityService.Core.V1.Contracts.Requests.Authorization; 

public class AuthorizeRequest {
	[Required]
	public string Token { get; set; }
}