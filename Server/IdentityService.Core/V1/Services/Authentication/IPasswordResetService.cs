using IdentityService.Core.V1.Contracts.Requests.Authentication;
using IdentityService.Core.V1.Contracts.Responses.Authentication;

namespace IdentityService.Core.V1.Services.Authentication; 

public interface IPasswordResetService {
	Task<RequestChangePasswordResponse> RequestChangePasswordTokenAsync(RequestChangePasswordRequest request);
	Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordRequest request);
}