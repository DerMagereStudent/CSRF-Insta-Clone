using CSRFInstaClone.Core.Contracts.Responses;

namespace CSRFInstaClone.Core.Contracts.External.Responses.IdentityService;

public class AuthorizeResponse : AuthorizeResponse<AuthorizeResponse.Body> {
	public new class Body : AuthorizeResponse<Body>.Body { }
}

public class AuthorizeResponse<TBody> : BaseResponse<TBody> where TBody : AuthorizeResponse<TBody>.Body {
	public class Body {
		public bool Authorized { get; set; }
	}
}