using CSRFInstaClone.Core.Entities;

namespace CSRFInstaClone.Core.Contracts.Responses.User; 

public class GetUserProfileResponse : BaseResponse<GetUserProfileResponse.Body> {
	public class Body {
		public UserProfile Profile { get; set; }
	}
}