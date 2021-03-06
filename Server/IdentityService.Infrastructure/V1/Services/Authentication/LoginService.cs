using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using IdentityService.Core.V1.Contracts.Requests.Authentication;
using IdentityService.Core.V1.Contracts.Responses.Authentication;
using IdentityService.Core.V1.Options;
using IdentityService.Core.V1.Services;
using IdentityService.Core.V1.Services.Authentication;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace IdentityService.Infrastructure.V1.Services.Authentication; 

public class LoginService : ILoginService<IdentityUser> {
	private readonly UserManager<IdentityUser> _userManager;
	private readonly IJwtIssuingService _jwtIssuingService;
	private readonly JwtIssuingOptions _jwtIssuingOptions;

	public LoginService(UserManager<IdentityUser> userManager, IJwtIssuingService jwtIssuingService, IOptions<JwtIssuingOptions> jwtIssuingOptions) {
		this._userManager = userManager;
		this._jwtIssuingService = jwtIssuingService;
		this._jwtIssuingOptions = jwtIssuingOptions.Value;
	}

	public async Task<LoginResponse> LoginUserAsync(LoginRequest request) {
		var user = await this._userManager.FindByEmailAsync(request.UsernameEmail);
			
		if (user is null) {
			user = await this._userManager.FindByNameAsync(request.UsernameEmail);

			if (user is null) {
				return new LoginResponse {
					Errors = new[] {LoginResponse.Error.UsernameOrEmailNotRegistered}
				};
			}
		}

		var validPassword = await this._userManager.CheckPasswordAsync(user, request.Password);

		if (!validPassword) {
			return new LoginResponse() {
				Errors = new[] {LoginResponse.Error.PasswordNotCorrect}
			};
		}

		var token = this._jwtIssuingService.CreateToken(
			this._jwtIssuingOptions,
			new[] {
				new Claim(JwtRegisteredClaimNames.Sub, user.Id),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Email, user.Email),
			}
		);

		return new LoginResponse() {
			Succeeded = true,
			Messages = new[] {LoginResponse.Message.LoggedIn},
			Content = new LoginResponse.Body() {
				Token = token,
			}
		};
	}

	public async Task<IdentityUser?> GetUserForTokenAsync(string token) {
		if (!this._jwtIssuingService.IsValidToken(token))
			return null;

		var claims = this._jwtIssuingService.GetClaimsFromToken(token)?.ToArray();

		var sub = claims?.FirstOrDefault(claim => claim.Type.Equals(JwtRegisteredClaimNames.Sub));

		if (sub is null)
			return null;

		return await this._userManager.FindByIdAsync(sub.Value);
	}
}