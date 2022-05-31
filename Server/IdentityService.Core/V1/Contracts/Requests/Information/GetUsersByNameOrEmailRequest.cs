using System.ComponentModel.DataAnnotations;

namespace IdentityService.Core.V1.Contracts.Requests.Information; 

public class GetUsersByNameOrEmailRequest {
	[Required] public string UsernameEmail { get; set; }
}