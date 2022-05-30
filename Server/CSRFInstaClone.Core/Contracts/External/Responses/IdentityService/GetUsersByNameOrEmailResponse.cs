using System.Collections.Generic;

using CSRFInstaClone.Core.Contracts.Responses;
using CSRFInstaClone.Core.Dtos.External;
using CSRFInstaClone.Core.ValueObjects;

namespace CSRFInstaClone.Core.Contracts.External.Responses.IdentityService; 

public class GetUsersByNameOrEmailResponse : BaseResponse<GetUsersByNameOrEmailResponse.Body> {
	public class Body {
		public IEnumerable<UserDto> Users { get; set; }
	}

	public static class Message {
		public static readonly Info SuitableUsersReturned = new Info {
			Code = nameof(SuitableUsersReturned),
			Description = "All suitable user for the specified query where returned successfully"
		};
	}
}