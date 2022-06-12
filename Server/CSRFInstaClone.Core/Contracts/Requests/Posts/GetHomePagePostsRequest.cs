namespace CSRFInstaClone.Core.Contracts.Requests.Posts; 

public class GetHomePagePostsRequest {
	public int Count { get; set; } = -1;
	public int Offset { get; set; } = -1;
}