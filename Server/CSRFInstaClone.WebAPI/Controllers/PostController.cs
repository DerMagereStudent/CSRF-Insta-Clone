using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using CSRFInstaClone.Core.Contracts.Requests.Posts;
using CSRFInstaClone.Core.Contracts.Responses.Posts;
using CSRFInstaClone.Core.Exceptions;
using CSRFInstaClone.Core.Services;
using CSRFInstaClone.Core.ValueObjects;
using CSRFInstaClone.WebAPI.Extensions;
using CSRFInstaClone.WebAPI.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace CSRFInstaClone.WebAPI.Controllers; 

[ApiController]
[Route("v1/post")]
public class PostController : ControllerBase {
	private readonly IPostService _postService;
	private readonly IIdentityService _identityService;

	public PostController(IPostService postService, IIdentityService identityService) {
		this._postService = postService;
		this._identityService = identityService;
	}
	
	[HttpGet]
	[Route("homepage")]
	[UserAuthenticated]
	public async Task<IActionResult> GetHomePagePostsAsync([FromQuery] GetHomePagePostsRequest requestBody) {
		try {
			return this.Ok(new GetHomePagePostsResponse {
				Succeeded = true,
				Messages = new[] {
					new Info {
						Code = "PostsReturned",
						Description = "The requested list of posts was returned successfully"
					}
				},
				Content = new GetHomePagePostsResponse.Body {
					Posts = await this._postService.GetHomePagePostsAsync(
						this._identityService.GetUserIdFromAuthToken(this.Request.Cookies[HeaderNames.Authorization]!)!,
						requestBody.Count,requestBody.Offset
					)
				}
			});
		}
		catch (InfoException e) {
			return this.InfoExceptionResponse<GetPostsResponse>(e);
		}
		catch (Exception) {
			return this.InternalServerErrorResponse<GetPostsResponse>();
		}
	}

	[HttpGet]
	[UserAuthenticated]
	public async Task<IActionResult> GetPostsAsync([FromQuery] GetPostsRequest requestBody) {
		try {
			var userId = requestBody.UserId ?? this._identityService.GetUserIdFromAuthToken(this.Request.Cookies[HeaderNames.Authorization]!)!;
			
			return this.Ok(new GetPostsResponse {
				Succeeded = true,
				Messages = new[] {
					new Info {
						Code = "PostsReturned",
						Description = "The requested list of posts was returned successfully"
					}
				},
				Content = new GetPostsResponse.Body {
					Posts = await this._postService.GetPostsAsync(userId, requestBody.Count, requestBody.Offset)
				}
			});
		}
		catch (InfoException e) {
			return this.InfoExceptionResponse<GetPostsResponse>(e);
		}
		catch (Exception) {
			return this.InternalServerErrorResponse<GetPostsResponse>();
		}
	}
	
	[HttpPost]
	[UserAuthenticated]
	public async Task<IActionResult> UploadPostAsync([FromBody] UploadPostRequest requestBody) {
		try {
			await this._postService.UploadPostAsync(
				this._identityService.GetUserIdFromAuthToken(this.Request.Cookies[HeaderNames.Authorization]!)!,
				requestBody.Description, requestBody.ImageIds
			);

			return this.Ok(new UploadPostResponse {
				Succeeded = true,
				Messages = new[] {
					new Info {
						Code = "PostUploaded",
						Description = "The post was uploaded successfully"
					}
				}
			});
		}
		catch (InfoException e) {
			return this.InfoExceptionResponse<UploadPostResponse>(e);
		}
		catch (Exception) {
			return this.InternalServerErrorResponse<UploadPostResponse>();
		}
	}

	[HttpDelete]
	[UserAuthenticated]
	public async Task<IActionResult> DeletePostAsync([FromBody] DeletePostRequest requestBody) {
		try {
			await this._postService.DeletePostAsync(this._identityService.GetUserIdFromAuthToken(this.Request.Cookies[HeaderNames.Authorization]!)!, requestBody.PostId);

			return this.Ok(new DeletePostResponse {
				Succeeded = true,
				Messages = new[] {
					new Info {
						Code = "PostDeleted",
						Description = "The post and the corresponding image were deleted successfully"
					}
				}
			});
		}
		catch (InfoException e) {
			return this.InfoExceptionResponse<DeletePostResponse>(e);
		}
		catch (Exception) {
			return this.InternalServerErrorResponse<DeletePostResponse>();
		}
	}
	
	[HttpGet]
	[Route("image")]
	public async Task<IActionResult> GetPostImageAsync([FromQuery] GetPostImageRequest requestBody) {
		try {
			var image = await this._postService.GetPostImageAsync(requestBody.ImageId);
			return this.File(image.Data, image.ImageType);
		}
		catch (Exception) {
			return this.StatusCode(500);
		}
	}
	
	[HttpPost]
	[Route("image")]
	[UserAuthenticated]
	public async Task<IActionResult> UploadImageAsync() {
		var imageFiles = this.Request.Form.Files.Where(f => f.ContentType.StartsWith("image")).ToList();
		
		if (imageFiles.Count == 0) {
			return this.BadRequest(new UploadImageResponse {
				Errors = new[] {
					new Info {
						Code = "MissingImageFile",
						Description = "No image file was sent"
					}
				}
			});
		}
		
		try {
			var imageIds = new string[imageFiles.Count];

			for (var i = 0; i < imageFiles.Count; i++) {
				var file = imageFiles[i];
				
				await using var ms = new MemoryStream();
				await file.CopyToAsync(ms);
				var fileBytes = ms.ToArray();

				imageIds[i] = await this._postService.UploadImageAsync(fileBytes, file.ContentType);
			}

			return this.Ok(new UploadImageResponse {
				Succeeded = true,
				Messages = new[] {
					new Info {
						Code = "ImagesUploaded",
						Description = "All image files were uploaded successfully"
					}
				},
				Content = new UploadImageResponse.Body {
					ImageIds = imageIds
				}
			});
		}
		catch (InfoException e) {
			return this.InfoExceptionResponse<UploadImageResponse>(e);
		}
		catch (Exception) {
			return this.InternalServerErrorResponse<UploadImageResponse>();
		}
	}
	
	[HttpGet]
	[Route("like/check")]
	[UserAuthenticated]
	public async Task<IActionResult> CheckLikeAsync([FromQuery] CheckLikeRequest requestBody) {
		try {
			return this.Ok(new CheckLikeReponse {
				Succeeded = true,
				Content = new CheckLikeReponse.Body() {
					PostLiked = await this._postService.CheckLikeAsync(
						this._identityService.GetUserIdFromAuthToken(this.Request.Cookies[HeaderNames.Authorization]!)!,
						requestBody.PostId
					)
				}
			});
		}
		catch (InfoException e) {
			return this.InfoExceptionResponse<CheckLikeReponse>(e);
		}
		catch (Exception) {
			return this.InternalServerErrorResponse<CheckLikeReponse>();
		}
	}
	
	[HttpPost]
	[Route("like")]
	[UserAuthenticated]
	public async Task<IActionResult> LikePostAsync([FromBody] LikePostRequest requestBody) {
		try {
			await this._postService.LikePostAsync(
				this._identityService.GetUserIdFromAuthToken(this.Request.Cookies[HeaderNames.Authorization]!)!,
				requestBody.PostId
			);
			
			return this.Ok(new LikePostResponse {
				Succeeded = true,
				Content = new LikePostResponse.Body()
			});
		}
		catch (InfoException e) {
			return this.InfoExceptionResponse<LikePostResponse>(e);
		}
		catch (Exception) {
			return this.InternalServerErrorResponse<LikePostResponse>();
		}
	}
	
	[HttpDelete]
	[Route("like")]
	[UserAuthenticated]
	public async Task<IActionResult> UnlikePostAsync([FromBody] UnlikePostRequest requestBody) {
		try {
			await this._postService.UnlikePostAsync(
				this._identityService.GetUserIdFromAuthToken(this.Request.Cookies[HeaderNames.Authorization]!)!,
				requestBody.PostId
			);
			
			return this.Ok(new UnlikePostResponse {
				Succeeded = true,
				Content = new UnlikePostResponse.Body()
			});
		}
		catch (InfoException e) {
			return this.InfoExceptionResponse<UnlikePostResponse>(e);
		}
		catch (Exception) {
			return this.InternalServerErrorResponse<UnlikePostResponse>();
		}
	}
}