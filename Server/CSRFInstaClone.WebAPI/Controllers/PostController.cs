using System;
using System.IO;
using System.Threading.Tasks;

using CSRFInstaClone.Core.Contracts.Requests.Posts;
using CSRFInstaClone.Core.Contracts.Responses.Posts;
using CSRFInstaClone.Core.Exceptions;
using CSRFInstaClone.Core.Services;
using CSRFInstaClone.Core.ValueObjects;
using CSRFInstaClone.WebAPI.Extensions;

using Microsoft.AspNetCore.Mvc;

namespace CSRFInstaClone.WebAPI.Controllers; 

[ApiController]
[Route("v1/post")]
public class PostController : ControllerBase {
	private readonly IPostService _postService;

	public PostController(IPostService postService) {
		this._postService = postService;
	}

	[HttpGet]
	public async Task<IActionResult> GetPostsAsync([FromQuery] GetPostsRequest requestBody) {
		try {
			return this.Ok(new GetPostsResponse {
				Succeeded = true,
				Messages = new[] {
					new Info {
						Code = "PostsReturned",
						Description = "The requested list of posts was returned successfully"
					}
				},
				Content = new GetPostsResponse.Body {
					Posts = await this._postService.GetPostsAsync(requestBody.UserId, requestBody.Count,
						requestBody.Offset)
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
	public async Task<IActionResult> UploadPostAsync([FromBody] UploadPostRequest requestBody) {
		try {
			await this._postService.UploadPostAsync(requestBody.UserId, requestBody.Description, requestBody.ImageId);

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
	public async Task<IActionResult> DeletePostAsync([FromBody] DeletePostRequest requestBody) {
		try {
			await this._postService.DeletePostAsync(requestBody.PostId);

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
			var image = await this._postService.GetPostImageAsync(requestBody.PostId);
			return this.File(image.Data, image.ImageType);
		}
		catch (Exception) {
			return this.StatusCode(500);
		}
	}
	
	[HttpPost]
	[Route("image")]
	public async Task<IActionResult> UploadImageAsync() {
		if (this.Request.Form.Files.Count == 0) {
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
			var file = this.Request.Form.Files[0];

			await using var ms = new MemoryStream();
			await file.CopyToAsync(ms);
			var fileBytes = ms.ToArray();

			return this.Ok(new UploadImageResponse {
				Succeeded = true,
				Messages = new[] {
					new Info {
						Code = "ImageUploaded",
						Description = "Image uploaded successfully"
					}
				},
				Content = new UploadImageResponse.Body {
					ImageId = await this._postService.UploadImageAsync(fileBytes, file.ContentType)
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
}