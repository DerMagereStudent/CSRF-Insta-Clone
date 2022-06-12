using System;
using System.Threading.Tasks;

using CSRFInstaClone.Core.Contracts.Requests;
using CSRFInstaClone.Core.Contracts.Requests.User;
using CSRFInstaClone.Core.Contracts.Responses.User;
using CSRFInstaClone.Core.Exceptions;
using CSRFInstaClone.Core.Services;
using CSRFInstaClone.Core.ValueObjects;
using CSRFInstaClone.WebAPI.Extensions;
using CSRFInstaClone.WebAPI.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace CSRFInstaClone.WebAPI.Controllers; 

[ApiController]
[Route("v1/user")]
public class UserController : ControllerBase {
	private readonly IUserService _userService;
	private readonly IIdentityService _identityService;
	private readonly ILogger<UserController> _logger;

	public UserController(IUserService userService, IIdentityService identityService, ILogger<UserController> logger) {
		this._userService = userService;
		this._identityService = identityService;
		this._logger = logger;
	}

	[HttpPost]
	[Route("signup")]
	public async Task<IActionResult> SignUpAsync([FromBody] SignUpRequest requestBody) {
		try {
			await this._userService.SignUpAsync(requestBody.Username, requestBody.Email, requestBody.Password);

			return this.Ok(new SignUpResponse {
				Succeeded = true,
				Messages = new []{new Info {
					Code = "SignedUp",
					Description = "You have been sign up and your profile was created"
				}}
			});
		}
		catch (InfoException e) {
			return this.InfoExceptionResponse<SignUpResponse>(e);
		}
		catch (Exception) {
			return this.InternalServerErrorResponse<SignUpResponse>();
		}
	}
	
	[HttpPut]
	[Route("biography")]
	[UserAuthenticated]
	public async Task<IActionResult> UpdateBiographyAsync([FromBody] UpdateBiographyRequest requestBody) {
		try {
			await this._userService.UpdateBiographyAsync(this._identityService.GetUserIdFromAuthToken(this.Request.Cookies[HeaderNames.Authorization]!)!, requestBody.Biography);
			
			return this.Ok(new UpdateBiographyResponse() {
				Succeeded = true,
				Messages = new []{new Info {
					Code = "BiographyUpdated",
					Description = "Successfully set your new biography"
				}}
			});
		}
		catch (InfoException e) {
			return this.InfoExceptionResponse<UpdateBiographyResponse>(e);
		}
		catch (Exception) {
			return this.InternalServerErrorResponse<UpdateBiographyResponse>();
		}
	}

	[HttpGet]
	[Route("profile")]
	[UserAuthenticated]
	public async Task<IActionResult> GetUserProfileAsync([FromQuery] GetUserProfileRequest requestBody) {
		try {
			var userId = requestBody.UserId ?? this._identityService.GetUserIdFromAuthToken(this.Request.Cookies[HeaderNames.Authorization]!)!;
			
			this._logger.LogInformation("UserId: {0}", userId);
			
			return this.Ok(new GetUserProfileResponse {
				Succeeded = true,
				Messages = new[] {
					new Info {
						Code = "ProfileReturned",
						Description = "The user profile info was returned successfully"
					}
				},
				Content = new GetUserProfileResponse.Body {
					Profile = await this._userService.GetUserProfileAsync(userId)
				}
			});
		}
		catch (InfoException e) {
			return this.InfoExceptionResponse<GetUserProfileResponse>(e);
		}
		catch (Exception e) {
			this._logger.LogError(e, null, Array.Empty<object?>());
			return this.InternalServerErrorResponse<GetUserProfileResponse>();
		}
	}

	[HttpPost]
	[Route("follow")]
	[UserAuthenticated]
	public async Task<IActionResult> FollowUserAsync([FromBody] FollowRequest requestBody) {
		try {
			await this._userService.FollowUserAsync(requestBody.UserId, this._identityService.GetUserIdFromAuthToken(this.Request.Cookies[HeaderNames.Authorization]!)!);

			return this.Ok(new FollowResponse {
				Succeeded = true,
				Messages = new[] {
					new Info {
						Code = "FollowingUser",
						Description = "Follow relationship added successfully"
					}
				}
			});
		}
		catch (InfoException e) {
			return this.InfoExceptionResponse<FollowResponse>(e);
		}
		catch (Exception) {
			return this.InternalServerErrorResponse<FollowResponse>();
		}
	}

	[HttpDelete]
	[Route("follow")]
	[UserAuthenticated]
	public async Task<IActionResult> UnfollowUserAsync([FromBody] UnfollowRequest requestBody) {
		try {
			await this._userService.UnfollowUserAsync(requestBody.UserId, this._identityService.GetUserIdFromAuthToken(this.Request.Cookies[HeaderNames.Authorization]!)!);

			return this.Ok(new UnfollowResponse {
				Succeeded = true,
				Messages = new[] {
					new Info {
						Code = "FollowingUser",
						Description = "Follow relationship added successfully"
					}
				}
			});
		}
		catch (InfoException e) {
			return this.InfoExceptionResponse<UnfollowResponse>(e);
		}
		catch (Exception) {
			return this.InternalServerErrorResponse<UnfollowResponse>();
		}
	}
}