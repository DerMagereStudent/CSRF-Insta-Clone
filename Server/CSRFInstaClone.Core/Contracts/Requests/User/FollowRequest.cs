using System.ComponentModel.DataAnnotations;

namespace CSRFInstaClone.Core.Contracts.Requests.User; 

public class FollowRequest {
	[Required] public string UserId { get; set; }
	[Required] public string FollowerId { get; set; }
}