using System.Threading.Tasks;

using CSRFInstaClone.Core.Entities;

namespace CSRFInstaClone.Core.Services; 

public interface IUserService {
	Task SignUpAsync();
	Task UpdateBiographyAsync(string biography);
	Task<UserProfile> GetUserProfileAsync(string userId);
	Task<byte[]> GetProfilePictureAsync(string userId);
	Task FollowUserAsync(string userId, string followerId);
	Task UnfollowUserAsync(string userId, string followerId);
}