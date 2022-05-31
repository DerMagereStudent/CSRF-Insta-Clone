using System.ComponentModel.DataAnnotations;

namespace IdentityService.Core.V1.Contracts.Requests.Authorization.Units;

public class UpdateOrganizationalUnitRequest {
	[Required] public string Id { get; set; }
	public string? Name { get; set; }
	public string? Type { get; set; }
}