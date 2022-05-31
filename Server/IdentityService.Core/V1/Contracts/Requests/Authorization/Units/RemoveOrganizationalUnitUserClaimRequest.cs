using System.ComponentModel.DataAnnotations;

namespace IdentityService.Core.V1.Contracts.Requests.Authorization.Units;

public class RemoveOrganizationalUnitUserClaimRequest {
	[Required] public string Id { get; set; }
}