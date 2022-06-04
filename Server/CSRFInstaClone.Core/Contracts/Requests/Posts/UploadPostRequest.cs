using System.ComponentModel.DataAnnotations;

namespace CSRFInstaClone.Core.Contracts.Requests.Posts; 

public class UploadPostRequest {
	[Required] public string Description { get; set; }
	[Required] public string[] ImageIds { get; set; }
}