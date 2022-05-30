using System.ComponentModel.DataAnnotations;

namespace CSRFInstaClone.Core.Contracts.External.Requests.IdentityService; 

public class GetUsersByNameOrEmailRequest {
	[Required] public string UsernameEmail { get; set; }
}