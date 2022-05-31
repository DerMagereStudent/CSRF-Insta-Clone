namespace CSRFInstaClone.Core.Contracts.Responses.Posts; 

public class UploadImageResponse : BaseResponse<UploadImageResponse.Body> {
	public class Body {
		public string ImageId { get; set; }
	}
}