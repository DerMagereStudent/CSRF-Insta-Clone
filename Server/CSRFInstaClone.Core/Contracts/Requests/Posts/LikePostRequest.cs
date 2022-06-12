using System.ComponentModel.DataAnnotations;

namespace CSRFInstaClone.Core.Contracts.Requests.Posts; 

public class LikePostRequest {
	[Required] public string PostId { get; set; }
}