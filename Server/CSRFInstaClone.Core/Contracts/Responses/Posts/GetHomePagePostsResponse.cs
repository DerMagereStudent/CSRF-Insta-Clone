using System.Collections.Generic;

using CSRFInstaClone.Core.Dtos;

namespace CSRFInstaClone.Core.Contracts.Responses.Posts; 

public class GetHomePagePostsResponse : BaseResponse<GetHomePagePostsResponse.Body> {
	public class Body {
		public List<PostDto> Posts { get; set; }
	}
}