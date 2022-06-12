namespace CSRFInstaClone.Core.Contracts.Responses.Posts; 

public class CheckLikeReponse : BaseResponse<CheckLikeReponse.Body> {
	public class Body {
		public bool PostLiked { get; set; }
	}
}