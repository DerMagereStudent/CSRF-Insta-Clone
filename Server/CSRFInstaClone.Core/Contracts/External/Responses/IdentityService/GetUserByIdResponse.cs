using CSRFInstaClone.Core.Contracts.Responses;
using CSRFInstaClone.Core.Dtos.External;

namespace CSRFInstaClone.Core.Contracts.External.Responses.IdentityService;

public class GetUserByIdResponse : BaseResponse<GetUserByIdResponse.Body> {
	public class Body {
		public UserDto User { get; set; }
	}
}