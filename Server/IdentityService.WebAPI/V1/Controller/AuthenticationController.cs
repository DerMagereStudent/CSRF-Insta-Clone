using IdentityService.Core.V1.Contracts.Requests.Authentication;
using IdentityService.Core.V1.Contracts.Responses.Authentication;
using IdentityService.Core.V1.Services.Authentication;
using IdentityService.WebAPI.V1.Extensions;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.WebAPI.V1.Controller; 

[ApiController]
[Route("v1/authentication")]
public class AuthenticationController : ControllerBase {
	private readonly ILogger<AuthenticationController> _logger;
	private readonly ISignUpService _signUpService;
	private readonly ILoginService<IdentityUser> _loginService;
	private readonly IPasswordResetService _passwordResetService;

	public AuthenticationController(ILogger<AuthenticationController> logger, ISignUpService signUpService, ILoginService<IdentityUser> loginService, IPasswordResetService passwordResetService) {
		this._logger = logger;
		this._signUpService = signUpService;
		this._loginService = loginService;
		this._passwordResetService = passwordResetService;
	}
/// <summary>
///	Signs up a user.
/// </summary>
[HttpPost]
	[Route("signup")]
	[ProducesResponseType(typeof(SignUpResponse), 200)]
	
	public async Task<IActionResult> SignUpUserAsync([FromBody] SignUpRequest request) {
		if (!this.HasValidModelState(out SignUpResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._signUpService.SignUpUserAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.SignUpUserAsync)} threw an exception");
			return this.InternalServerError<SignUpResponse>(e);
		}
	}
	/// <summary>
	/// Logs a user in.
	/// </summary>
	[HttpPost]
	[Route("login")]
	[ProducesResponseType(typeof(LoginResponse), 200)]
	public async Task<IActionResult> LoginUserAsync([FromBody] LoginRequest request) {
		if (!this.HasValidModelState(out LoginResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._loginService.LoginUserAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.LoginUserAsync)} threw an exception");
			return this.InternalServerError<LoginResponse>(e);
		}
	}
	/// <summary>
	/// Requests a reset token via email.
	/// </summary>
	[HttpPost]
	[Route("requestresetpwd")]
	[ProducesResponseType(typeof(RequestChangePasswordResponse), 200)]
	public async Task<IActionResult> RequestResetPasswordAsync([FromBody] RequestChangePasswordRequest request) {
		if (!this.HasValidModelState(out RequestChangePasswordResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._passwordResetService.RequestChangePasswordTokenAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.RequestResetPasswordAsync)} threw an exception");
			return this.InternalServerError<RequestChangePasswordResponse>(e);
		}
	}
	/// <summary>
	/// resets a password of a user.
	/// </summary>
	[HttpPost]
	[Route("resetpwd")]
	[ProducesResponseType(typeof(ChangePasswordResponse), 200)]
	public async Task<IActionResult> ResetPasswordAsync([FromBody] ChangePasswordRequest request) {
		if (!this.HasValidModelState(out ChangePasswordResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._passwordResetService.ChangePasswordAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.LoginUserAsync)} threw an exception");
			return this.InternalServerError<ChangePasswordResponse>(e);
		}
	}
}