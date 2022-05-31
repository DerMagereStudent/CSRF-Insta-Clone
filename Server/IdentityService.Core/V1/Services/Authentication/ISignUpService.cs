using IdentityService.Core.V1.Contracts.Requests.Authentication;
using IdentityService.Core.V1.Contracts.Responses.Authentication;

namespace IdentityService.Core.V1.Services.Authentication; 

public interface ISignUpService {
	Task<SignUpResponse> SignUpUserAsync(SignUpRequest request);
}