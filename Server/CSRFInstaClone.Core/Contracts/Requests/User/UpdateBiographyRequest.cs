using System.ComponentModel.DataAnnotations;

namespace CSRFInstaClone.Core.Contracts.Requests.User; 

public class UpdateBiographyRequest {
	[Required] public string Biography { get; set; }
}