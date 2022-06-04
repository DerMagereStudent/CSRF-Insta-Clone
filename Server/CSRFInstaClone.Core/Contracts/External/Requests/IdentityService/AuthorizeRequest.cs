using System.ComponentModel.DataAnnotations;

namespace CSRFInstaClone.Core.Contracts.External.Requests.IdentityService; 

public class AuthorizeRequest {
	[Required]
	public string Token { get; set; }
}