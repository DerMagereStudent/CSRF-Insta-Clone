using System.ComponentModel.DataAnnotations;

namespace CSRFInstaClone.Core.Contracts.Requests.Posts; 

public class CheckLikeRequest {
	[Required] public string PostId { get; set; }
}