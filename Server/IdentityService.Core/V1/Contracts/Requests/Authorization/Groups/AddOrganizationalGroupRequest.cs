using System.ComponentModel.DataAnnotations;

namespace IdentityService.Core.V1.Contracts.Requests.Authorization.Groups; 

public class AddOrganizationalGroupRequest {
	[Required] public string Name { get; set; }
}