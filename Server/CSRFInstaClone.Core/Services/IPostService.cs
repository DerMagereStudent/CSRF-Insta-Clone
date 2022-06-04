using System.Collections.Generic;
using System.Threading.Tasks;

using CSRFInstaClone.Core.Entities;

namespace CSRFInstaClone.Core.Services; 

public interface IPostService {
	Task<string> UploadImageAsync(byte[] data, string imageType);
	Task UploadPostAsync(string userId, string description, string[] imageIds);
	Task DeletePostAsync(string userId, string postId);
	Task<List<Post>> GetPostsAsync(string userId, int count, int offset);
	Task<Image> GetPostImageAsync(string imageId);
}