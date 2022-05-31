using System.ComponentModel.DataAnnotations;

namespace CSRFInstaClone.Core.Contracts.Requests.Posts; 

public class UploadPostRequest {
	[Required] public string UserId { get; set; }
	[Required] public string Description { get; set; }
	[Required] public string ImageId { get; set; }
}