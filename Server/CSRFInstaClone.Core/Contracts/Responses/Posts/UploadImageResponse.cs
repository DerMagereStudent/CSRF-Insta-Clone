namespace CSRFInstaClone.Core.Contracts.Responses.Posts; 

public class UploadImageResponse : BaseResponse<UploadImageResponse.Body> {
	public class Body {
		public string[] ImageIds { get; set; }
	}
}