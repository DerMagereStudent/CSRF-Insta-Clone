using System.ComponentModel.DataAnnotations;

namespace CSRFInstaClone.Core.Contracts.Requests.Posts; 

public class DeletePostRequest {
	[Required] public string PostId { get; set; }
}