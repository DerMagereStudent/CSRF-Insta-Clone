using System.Collections.Generic;

using CSRFInstaClone.Core.Dtos;

namespace CSRFInstaClone.Core.Contracts.Responses.Posts; 

public class GetPostsResponse : BaseResponse<GetPostsResponse.Body> {
	public class Body {
		public List<PostDto> Posts { get; set; }
	}
}