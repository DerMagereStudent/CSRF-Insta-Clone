using System.ComponentModel.DataAnnotations;

namespace CSRFInstaClone.Core.Contracts.Requests.Posts; 

public class UnlikePostRequest {
	[Required] public string PostId { get; set; }
}