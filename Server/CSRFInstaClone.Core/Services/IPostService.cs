using System.Collections.Generic;
using System.Threading.Tasks;

using CSRFInstaClone.Core.Entities;

namespace CSRFInstaClone.Core.Services; 

public interface IPostService {
	Task<string> UploadImageAsync(byte[] data, string imageData);
	Task UploadPostAsync(string userId, string description, string imageId);
	Task DeletePostAsync(string postId);
	Task<List<Post>> GetPostsAsync(string userId, int count, int offset);
	Task<Image> GetPostImageAsync(string postId);
}