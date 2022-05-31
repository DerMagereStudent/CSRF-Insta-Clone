using IdentityService.Core.V1.ValueObjects;

namespace IdentityService.Core.V1.Contracts.Responses.Authentication;

public class SignUpResponse : BaseResponse<SignUpResponse.Body> {
	public class Body { }
	
	public static class Message {
		public static readonly Info SignedUp = new() {
			Code = nameof(SignedUp),
			Description = "You have been signed up"
		};
	}
}