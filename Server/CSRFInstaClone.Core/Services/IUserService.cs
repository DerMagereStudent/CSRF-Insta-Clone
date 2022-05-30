using System.Threading.Tasks;

using CSRFInstaClone.Core.Entities;

namespace CSRFInstaClone.Core.Services; 

public interface IUserService {
	Task SignUpAsync(string username, string email, string password);
	Task UpdateBiographyAsync(string userId, string biography);
	Task<UserProfile> GetUserProfileAsync(string userId);
	Task FollowUserAsync(string userId, string followerId);
	Task UnfollowUserAsync(string userId, string followerId);
}