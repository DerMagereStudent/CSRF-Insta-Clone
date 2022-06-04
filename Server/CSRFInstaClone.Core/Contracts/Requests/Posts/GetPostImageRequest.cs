using System.ComponentModel.DataAnnotations;

namespace CSRFInstaClone.Core.Contracts.Requests.Posts; 

public class GetPostImageRequest {
	[Required] public string ImageId { get; set; }
}