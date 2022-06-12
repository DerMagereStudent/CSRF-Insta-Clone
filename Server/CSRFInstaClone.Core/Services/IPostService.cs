using System.Collections.Generic;
using System.Threading.Tasks;

using CSRFInstaClone.Core.Dtos;
using CSRFInstaClone.Core.Entities;

namespace CSRFInstaClone.Core.Services; 

public interface IPostService {
	Task<string> UploadImageAsync(byte[] data, string imageType);
	Task UploadPostAsync(string userId, string description, string[] imageIds);
	Task DeletePostAsync(string userId, string postId);
	Task<List<PostDto>> GetHomePagePostsAsync(string userId, int count, int offset);
	Task<List<PostDto>> GetPostsAsync(string userId, int count, int offset);
	Task<Image> GetPostImageAsync(string imageId);
	Task LikePostAsync(string userId, string postId);
	Task UnlikePostAsync(string userId, string postId);
	Task<bool> CheckLikeAsync(string userId, string postId);
}