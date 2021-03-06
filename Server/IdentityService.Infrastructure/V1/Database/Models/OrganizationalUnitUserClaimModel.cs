using IdentityService.Core.V1.Entities;

using Microsoft.AspNetCore.Identity;

namespace IdentityService.Infrastructure.V1.Database.Models;

public class OrganizationalUnitUserClaimModel : OrganizationalUnitUserClaim {
	public OrganizationalUnitModel Unit { get; set; } 
	public IdentityUser User { get; set; } 
}