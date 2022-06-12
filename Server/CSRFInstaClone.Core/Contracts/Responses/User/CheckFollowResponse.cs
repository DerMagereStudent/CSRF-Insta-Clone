namespace CSRFInstaClone.Core.Contracts.Responses.User; 

public class CheckFollowResponse : BaseResponse<CheckFollowResponse.Body> {
	public class Body {
		public bool Following { get; set; }
	}
}