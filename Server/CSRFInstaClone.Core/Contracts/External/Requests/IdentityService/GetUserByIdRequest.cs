using System.ComponentModel.DataAnnotations;

namespace CSRFInstaClone.Core.Contracts.External.Requests.IdentityService;

public class GetUserByIdRequest {
	[Required] public string UserId { get; set; }
}