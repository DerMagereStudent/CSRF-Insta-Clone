using System.ComponentModel.DataAnnotations;

namespace IdentityService.Core.V1.Contracts.Requests.Authorization.Groups;

public class UpdateOrganizationalGroupRequest {
	[Required] public string Id { get; set; }
	[Required] public string Name { get; set; }
}