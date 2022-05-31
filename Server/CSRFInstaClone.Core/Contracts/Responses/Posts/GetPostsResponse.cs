using System.Collections.Generic;

using CSRFInstaClone.Core.Entities;

namespace CSRFInstaClone.Core.Contracts.Responses.Posts; 

public class GetPostsResponse : BaseResponse<GetPostsResponse.Body> {
	public class Body {
		public List<Post> Posts { get; set; }
	}
}