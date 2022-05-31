using IdentityService.Core.V1.Contracts.Requests.Information;
using IdentityService.Core.V1.Contracts.Responses.Information;

namespace IdentityService.Core.V1.Services.Information; 

public interface IAuthenticationInformationService {
	Task<GetUserByIdResponse> GetUserByIdAsync(GetUserByIdRequest request);
	Task<GetUsersByNameOrEmailResponse> GetUsersByNameOrEmailAsync(GetUsersByNameOrEmailRequest request);
}