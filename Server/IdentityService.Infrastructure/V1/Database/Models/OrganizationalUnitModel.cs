using IdentityService.Core.V1.Entities;

namespace IdentityService.Infrastructure.V1.Database.Models;

public class OrganizationalUnitModel : OrganizationalUnit {
	public OrganizationalGroupModel Group { get; set; } 
	public ICollection<OrganizationalUnitUserClaimModel> UserClaims { get; set; }
}