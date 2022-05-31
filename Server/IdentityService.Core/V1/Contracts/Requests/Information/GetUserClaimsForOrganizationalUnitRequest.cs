using System.ComponentModel.DataAnnotations;

namespace IdentityService.Core.V1.Contracts.Requests.Information;

public class GetUserClaimsForOrganizationalUnitRequest {
	[Required] public string UnitId { get; set; }
}