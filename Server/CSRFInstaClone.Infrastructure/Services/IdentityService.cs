using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using CSRFInstaClone.Core.Contracts.External.Requests.IdentityService;
using CSRFInstaClone.Core.Contracts.External.Responses.IdentityService;
using CSRFInstaClone.Core.Dtos.External;
using CSRFInstaClone.Core.Exceptions;
using CSRFInstaClone.Core.Options;
using CSRFInstaClone.Core.Services;
using CSRFInstaClone.Core.ValueObjects;
using CSRFInstaClone.Infrastructure.Extensions;

using Microsoft.Extensions.Options;

namespace CSRFInstaClone.Infrastructure.Services;

public class IdentityService : IIdentityService {
	private readonly IOptions<GatewayOptions> _gatewayOptions;

	public IdentityService(IOptions<GatewayOptions> gatewayOptions) {
		this._gatewayOptions = gatewayOptions;
	}

	public async Task SignUpAsync(string username, string email, string password) {
		using var httpClient = new HttpClient();
		var signUpResponse = await httpClient.SendPostAsync<SignUpRequest, SignUpResponse>(
			this._gatewayOptions.Value.Routes.IdentityServiceSignUp,
			new SignUpRequest {
				Username = username,
				Email = email,
				Password = password
			}
		);

		if (signUpResponse is null) {
			throw new InfoException(new List<Info> {new() {
				Code = "IdentityServiceInaccessible",
				Description = "Could not communicate with the Identity-Service"
			}});
		}

		if (!signUpResponse.Succeeded)
			throw new InfoException(signUpResponse.Errors.ToList());
	}

	public async Task<UserDto> GetUserById(string userId) {
		using var httpClient = new HttpClient();

		var getUserResponse = await httpClient.SendGetAsync<GetUserByIdResponse>(string.Format("{0}?{1}={2}",
			this._gatewayOptions.Value.Routes.IdentityServiceInformationGetUserById, nameof(GetUserByIdRequest.UserId), userId
		));
		
		if (getUserResponse is null) {
			throw new InfoException(new List<Info> {new() {
				Code = "IdentityServiceInaccessible",
				Description = "Could not communicate with the Identity-Service"
			}});
		}

		if (!getUserResponse.Succeeded)
			throw new InfoException(getUserResponse.Errors.ToList());

		return getUserResponse.Content!.User;
	}

	public async Task<List<UserDto>> GetUsersByNameOrEmailAsync(string usernameEmail) {
		using var httpClient = new HttpClient();
		
		var getUserResponse = await httpClient.SendGetAsync<GetUsersByNameOrEmailResponse>(string.Format("{0}?{1}={2}",
			this._gatewayOptions.Value.Routes.IdentityServiceInformationGetUsersByNameOrEmail, nameof(GetUsersByNameOrEmailRequest.UsernameEmail), usernameEmail
		));
		
		if (getUserResponse is null) {
			throw new InfoException(new List<Info> {new() {
				Code = "IdentityServiceInaccessible",
				Description = "Could not communicate with the Identity-Service"
			}});
		}

		if (!getUserResponse.Succeeded)
			throw new InfoException(getUserResponse.Errors.ToList());

		return getUserResponse.Content!.Users.ToList();
	}

	public string? GetUserIdFromAuthToken(string token) {
		var securityToken = new JwtSecurityTokenHandler().ReadToken(token);
		
		if (securityToken is not JwtSecurityToken jwtSecurityToken)
			return null;
		
		return jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type.Equals(JwtRegisteredClaimNames.Sub))?.Value;
	}
}