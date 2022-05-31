using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CSRFInstaClone.Core.Entities;
using CSRFInstaClone.Core.Exceptions;
using CSRFInstaClone.Core.Services;
using CSRFInstaClone.Core.ValueObjects;
using CSRFInstaClone.Infrastructure.Database;

using Microsoft.EntityFrameworkCore;

namespace CSRFInstaClone.Infrastructure.Services; 

public class PostService : IPostService {
	private readonly ApplicationDbContext _applicationDbContext;
	private readonly IIdentityService _identityService;

	public PostService(ApplicationDbContext applicationDbContext, IIdentityService identityService) {
		this._applicationDbContext = applicationDbContext;
		this._identityService = identityService;
	}
	
	public async Task<string> UploadImageAsync(byte[] data, string imageType) {
		var image = new Image {
			Id = Guid.NewGuid().ToString(),
			Data = data,
			ImageType = imageType
		};

		this._applicationDbContext.Images.Add(image);
		await this._applicationDbContext.SaveChangesAsync();
		return image.Id;
	}

	public async Task UploadPostAsync(string userId, string description, string imageId) {
		//await this._identityService.GetUserById(userId);

		var image = await this._applicationDbContext.Images.FindAsync(imageId);

		if (image is null) {
			throw new InfoException(new List<Info> {
				new Info {
					Code = "ImageDoesNotExist",
					Description = "The image that should be linked to the post does not exist"
				}
			});
		}

		var post = new Post {
			Id = Guid.NewGuid().ToString(),
			UserId = userId,
			ImageId = imageId,
			Description = description,
			DateTimePosted = DateTime.UtcNow
		};

		this._applicationDbContext.Posts.Add(post);
		await this._applicationDbContext.SaveChangesAsync();
	}

	public async Task DeletePostAsync(string postId) {
		var post = await this._applicationDbContext.Posts.FindAsync(postId);

		if (post is null) {
			throw new InfoException(new List<Info> {
				new Info {
					Code = "PostDoesNotExist",
					Description = "The post that should be deleted does not exist"
				}
			});
		}

		var image = await this.GetPostImageAsync(post.ImageId);

		this._applicationDbContext.Images.Remove(image);
		this._applicationDbContext.Posts.Remove(post);
		await this._applicationDbContext.SaveChangesAsync();
	}

	public async Task<List<Post>> GetPostsAsync(string userId, int count, int offset) {
		IQueryable<Post> query = this._applicationDbContext.Posts
			.Where(p => p.UserId.Equals(userId))
			.OrderByDescending(p => p.DateTimePosted);

		if (offset >= 0)
			query = query.Skip(offset);

		if (count >= 0)
			query = query.Take(count);

		return await query.ToListAsync();
	}

	public async Task<Image> GetPostImageAsync(string postId) {
		var post = await this._applicationDbContext.Posts.FindAsync(postId);

		if (post is null) {
			throw new InfoException(new List<Info> {
				new Info {
					Code = "PostDoesNotExist",
					Description = "The post  does not exist"
				}
			});
		}

		return (await this._applicationDbContext.Images.FindAsync(post.ImageId))!;
	}
}