using IdentityService.Core.V1.Contracts.Requests.Authorization;
using IdentityService.Core.V1.Contracts.Responses.Authorization;

namespace IdentityService.Core.V1.Services.Authorization; 

public interface IGlobalAuthorizationService {
	Task<AuthorizeResponse> AuthorizeUserAsync(AuthorizeRequest request);
	Task<AuthorizeGlobalRoleResponse> AuthorizeUserGlobalRoleAsync(AuthorizeGlobalRoleRequest request);
}