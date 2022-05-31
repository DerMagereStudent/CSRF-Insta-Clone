using IdentityService.Core.V1.Contracts.Requests.Authorization.Units;
using IdentityService.Core.V1.Contracts.Responses.Authorization.Units;

namespace IdentityService.Core.V1.Services.Authorization; 

public interface IEntityAuthorizationService {
	Task<ValidateOrganizationalUnitUserClaimResponse> ValidateOrganizationalUnitUserClaimAsync(ValidateOrganizationalUnitUserClaimRequest request);
}