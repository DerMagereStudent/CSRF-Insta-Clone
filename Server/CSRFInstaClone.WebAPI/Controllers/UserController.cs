﻿using System;
using System.Threading.Tasks;

using CSRFInstaClone.Core.Contracts.Requests;
using CSRFInstaClone.Core.Contracts.Requests.User;
using CSRFInstaClone.Core.Contracts.Responses.User;
using CSRFInstaClone.Core.Exceptions;
using CSRFInstaClone.Core.Services;
using CSRFInstaClone.Core.ValueObjects;
using CSRFInstaClone.WebAPI.Extensions;

using Microsoft.AspNetCore.Mvc;

namespace CSRFInstaClone.WebAPI.Controllers; 

[ApiController]
[Route("v1/user")]
public class UserController : ControllerBase {
	private readonly IUserService _userService;

	public UserController(IUserService userService) {
		this._userService = userService;
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
	public async Task<IActionResult> UpdateBiographyAsync([FromBody] UpdateBiographyRequest requestBody) {
		try {
			await this._userService.UpdateBiographyAsync(requestBody.UserId, requestBody.Biography);
			
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
	public async Task<IActionResult> GetUserProfileAsync([FromQuery] GetUserProfileRequest requestBody) {
		try {
			return this.Ok(new GetUserProfileResponse {
				Succeeded = true,
				Messages = new[] {
					new Info {
						Code = "ProfileReturned",
						Description = "The user profile info was returned successfully"
					}
				},
				Content = new GetUserProfileResponse.Body {
					Profile = await this._userService.GetUserProfileAsync(requestBody.UserId)
				}
			});
		}
		catch (InfoException e) {
			return this.InfoExceptionResponse<GetUserProfileResponse>(e);
		}
		catch (Exception) {
			return this.InternalServerErrorResponse<GetUserProfileResponse>();
		}
	}

	[HttpPost]
	[Route("follow")]
	public async Task<IActionResult> FollowUserAsync([FromBody] FollowRequest requestBody) {
		try {
			await this._userService.FollowUserAsync(requestBody.UserId, requestBody.FollowerId);

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
	public async Task<IActionResult> UnfollowUserAsync([FromBody] UnfollowRequest requestBody) {
		try {
			await this._userService.UnfollowUserAsync(requestBody.UserId, requestBody.FollowerId);

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