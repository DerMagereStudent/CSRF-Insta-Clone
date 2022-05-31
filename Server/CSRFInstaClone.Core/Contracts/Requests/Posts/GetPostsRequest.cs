using System.ComponentModel.DataAnnotations;

namespace CSRFInstaClone.Core.Contracts.Requests.Posts; 

public class GetPostsRequest {
	[Required] public string UserId { get; set; }
	public int Count { get; set; } = -1;
	public int Offset { get; set; } = -1;
}