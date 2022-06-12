using System.ComponentModel.DataAnnotations;

namespace CSRFInstaClone.Core.Contracts.Requests.User; 

public class CheckFollowRequest {
	[Required] public string UserId { get; set; }
}