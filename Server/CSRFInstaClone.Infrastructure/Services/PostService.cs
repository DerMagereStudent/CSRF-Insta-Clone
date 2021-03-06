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

		return await this.GetPostsAsync(userId, count, offset, q => q.Where(p => followingUsers.Contains(p.UserId)));
	}

	public async Task<List<PostDto>> GetPostsAsync(string userId, int count, int offset) {
		return await this.GetPostsAsync(userId, count, offset, q => q.Where(p => p.UserId.Equals(userId)));
	}

	private async Task<List<PostDto>> GetPostsAsync(string userId, int count, int offset, Func<IQueryable<Post>, IQueryable<Post>> whereBuilder) {
		IQueryable<Post> query = this._applicationDbContext.Posts
			.Include(p => p.Images);
			
		query = whereBuilder(query);
			
		IQueryable<PostDto> finalQuery = query 
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
			finalQuery = finalQuery.Skip(offset);

		if (count >= 0)
			finalQuery = finalQuery.Take(count);

		var posts = await finalQuery.ToListAsync();

		foreach (var post in posts)
			post.Likes = await this._applicationDbContext.Likes.Where(l => l.PostId.Equals(post.Id)).CountAsync();
		
		return posts;
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
	
	public async Task LikePostAsync(string userId, string postId) {
		await this._identityService.GetUserById(userId);

		var post = await this._applicationDbContext.Posts.FindAsync(postId);
		
		if (post is null) {
			throw new InfoException(new List<Info> {
				new Info {
					Code = "PostDoesNotExist",
					Description = "The post does not exist"
				}
			});
		}
		
		var like = await this._applicationDbContext.Likes.FindAsync(postId, userId);

		if (like is not null)
			return;

		this._applicationDbContext.Likes.Add(new Like {
			UserId = userId,
			PostId = postId
		});
		await this._applicationDbContext.SaveChangesAsync();
	}
	
	public async Task UnlikePostAsync(string userId, string postId) {
		await this._identityService.GetUserById(userId);
		
		var post = await this._applicationDbContext.Posts.FindAsync(postId);
		
		if (post is null) {
			throw new InfoException(new List<Info> {
				new Info {
					Code = "PostDoesNotExist",
					Description = "The post does not exist"
				}
			});
		}

		var like = await this._applicationDbContext.Likes.FindAsync(postId, userId);

		if (like is null)
			return;

		this._applicationDbContext.Likes.Remove(like);
		await this._applicationDbContext.SaveChangesAsync();
	}
	
	public async Task<bool> CheckLikeAsync(string userId, string postId) {
		return (await this._applicationDbContext.Likes.FindAsync(postId, userId)) is not null;
	}
}