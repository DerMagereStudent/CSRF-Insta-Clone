using System.ComponentModel.DataAnnotations;

namespace CSRFInstaClone.Core.Contracts.Requests.User; 

public class GetUserProfileRequest {
	[Required] public string UserId { get; set; }
}