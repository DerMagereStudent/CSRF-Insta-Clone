using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CSRFInstaClone.Core.Dtos;
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

	public async Task UploadPostAsync(string userId, string description, string[] imageIds) {
		await this._identityService.GetUserById(userId);

		var images = await this._applicationDbContext.Images.Where(i => imageIds.Contains(i.Id)).ToListAsync();

		if (images.Count == 0) {
			throw new InfoException(new List<Info> {
				new Info {
					Code = "NoImagesToLink",
					Description = "There are no existing images to link with the post"
				}
			});
		}

		var post = new Post {
			Id = Guid.NewGuid().ToString(),
			UserId = userId,
			Description = description,
			DateTimePosted = DateTime.UtcNow
		};

		foreach (var image in images)
			image.PostId = post.Id;

		this._applicationDbContext.Posts.Add(post);
		await this._applicationDbContext.SaveChangesAsync();
	}

	public async Task DeletePostAsync(string userId, string postId) {
		var post = await this._applicationDbContext.Posts.FindAsync(postId);

		if (post is null) {
			throw new InfoException(new List<Info> {
				new Info {
					Code = "PostDoesNotExist",
					Description = "The post that should be deleted does not exist"
				}
			});
		}

		if (!post.UserId.Equals(userId)) {
			throw new InfoException(new List<Info> {
				new Info {
					Code = "Unauthorized",
					Description = "The post that should be deleted is not owned by the specified user"
				}
			});
		}

		this._applicationDbContext.Posts.Remove(post);
		await this._applicationDbContext.SaveChangesAsync();
	}

	public async Task<List<PostDto>> GetHomePagePostsAsync(string userId, int count, int offset) {
		var followingUsers = await this._applicationDbContext.Followers
			.Where(f => f.FollowerId.Equals(userId))
			.Select(f => f.UserId).ToListAsync();
		
		IQueryable<PostDto> postsQuery = this._applicationDbContext.Posts
			.Include(p => p.Images)
			.Where(p => followingUsers.Contains(p.UserId))
			.Select(p => new PostDto {
				Id = p.Id,
				UserId = p.UserId,
				Description = p.Description,
				DateTimePosted = p.DateTimePosted,
				Images = p.Images.Select(i => new ImageDto {
					Id = i.Id,
					PostId = i.PostId
				}).ToList()
			})
			.OrderByDescending(p => p.DateTimePosted);

		if (offset > 0)
			postsQuery = postsQuery.Skip(offset);

		if (count > 0)
			postsQuery = postsQuery.Take(count);

		return await postsQuery.ToListAsync();
	}

	public async Task<List<PostDto>> GetPostsAsync(string userId, int count, int offset) {
		IQueryable<PostDto> query = this._applicationDbContext.Posts
			.Include(p => p.Images)
			.Where(p => p.UserId.Equals(userId))
			.Select(p => new PostDto {
				Id = p.Id,
				UserId = p.UserId,
				Description = p.Description,
				DateTimePosted = p.DateTimePosted,
				Images = p.Images.Select(i => new ImageDto {
					Id = i.Id,
					PostId = i.PostId
				}).ToList()
			})
			.OrderByDescending(p => p.DateTimePosted);

		if (offset >= 0)
			query = query.Skip(offset);

		if (count >= 0)
			query = query.Take(count);

		return await query.ToListAsync();
	}

	public async Task<Image> GetPostImageAsync(string imageId) {
		var image = await this._applicationDbContext.Images.FindAsync(imageId);

		if (image is null) {
			throw new InfoException(new List<Info> {
				new Info {
					Code = "ImageDoesNotExist",
					Description = "The image does not exist"
				}
			});
		}

		return image;
	}
}