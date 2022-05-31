using System.ComponentModel.DataAnnotations;

namespace IdentityService.Core.V1.Contracts.Requests.Information;

public class GetOrganizationalGroupByNameRequest {
	[Required] public string GroupName { get; set; }
}