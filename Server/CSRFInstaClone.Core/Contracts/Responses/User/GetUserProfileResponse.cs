using CSRFInstaClone.Core.Dtos;

namespace CSRFInstaClone.Core.Contracts.Responses.User; 

public class GetUserProfileResponse : BaseResponse<GetUserProfileResponse.Body> {
	public class Body {
		public UserProfileDto Profile { get; set; }
	}
}