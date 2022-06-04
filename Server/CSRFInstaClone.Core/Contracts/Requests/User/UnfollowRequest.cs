using System.ComponentModel.DataAnnotations;

namespace CSRFInstaClone.Core.Contracts.Requests.User; 

public class UnfollowRequest {
	[Required] public string UserId { get; set; }
}