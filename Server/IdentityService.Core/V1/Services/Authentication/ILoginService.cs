using IdentityService.Core.V1.Contracts.Requests.Authentication;
using IdentityService.Core.V1.Contracts.Responses.Authentication;

namespace IdentityService.Core.V1.Services.Authentication; 

public interface ILoginService<TUser> {
	Task<LoginResponse> LoginUserAsync(LoginRequest request);
	Task<TUser?> GetUserForTokenAsync(string token);
}