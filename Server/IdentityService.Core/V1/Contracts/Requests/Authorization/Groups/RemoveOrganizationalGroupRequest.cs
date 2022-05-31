using System.ComponentModel.DataAnnotations;

namespace IdentityService.Core.V1.Contracts.Requests.Authorization.Groups;

public class RemoveOrganizationalGroupRequest {
	[Required] public string Id { get; set; }
}