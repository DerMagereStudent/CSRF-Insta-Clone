using System;
using System.Linq;
using System.Threading.Tasks;

using CSRFInstaClone.Core.Dtos;
using CSRFInstaClone.Core.Entities;
using CSRFInstaClone.Core.Options;
using CSRFInstaClone.Core.Services;
using CSRFInstaClone.Infrastructure.Database;

using Mapster;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CSRFInstaClone.Infrastructure.Services; 

public class UserService : IUserService {
	private readonly ApplicationDbContext _applicationDbContext;
	private readonly IIdentityService _identityService;
	private readonly IOptions<GatewayOptions> _gatewayOptions;
	private readonly ILogger<UserService> _logger;

	public UserService(ApplicationDbContext applicationDbContext, IIdentityService identityService, IOptions<GatewayOptions> gatewayOptions, ILogger<UserService> logger) {
		this._applicationDbContext = applicationDbContext;
		this._identityService = identityService;
		this._gatewayOptions = gatewayOptions;
		this._logger = logger;
	}
	
	public async Task SignUpAsync(string username, string email, string password) {
		await this._identityService.SignUpAsync(username, email, password);

		var users = await this._identityService.GetUsersByNameOrEmailAsync(username);
		var userId = users.First(u => u.Username.Equals(username)).Id;
		
		this._logger.LogInformation("Profile User Id: {0}", userId);
		
		var userProfile = new UserProfile {
			Id = Guid.NewGuid().ToString(),
			UserId = userId,
			DisplayName = username,
			Biography = ""
		};

		this._applicationDbContext.UserProfiles.Add(userProfile);
		await this._applicationDbContext.SaveChangesAsync();
	}

	public async Task UpdateBiographyAsync(string userId, string biography) {
		var userProfile = await this._applicationDbContext.UserProfiles.FirstAsync(up => up.UserId.Equals(userId));

		userProfile.Biography = biography;
		await this._applicationDbContext.SaveChangesAsync();
	}

	public async Task<UserProfileDto> GetUserProfileAsync(string userId) {
		var profile = await this._applicationDbContext.UserProfiles.FirstAsync(up => up.UserId.Equals(userId));
		var profileDto = profile.Adapt<UserProfileDto>();
		profileDto.Followers = await this._applicationDbContext.Followers.Where(f => f.UserId.Equals(userId)).CountAsync();
		profileDto.Following = await this._applicationDbContext.Followers.Where(f => f.FollowerId.Equals(userId)).CountAsync();
		return profileDto;
	}

	public async Task FollowUserAsync(string userId, string followerId) {
		// Both will throw InfoException if not found
		await this._identityService.GetUserById(userId);
		await this._identityService.GetUserById(followerId);
		
		var newFollower = new Follower {
			UserId = userId,
			FollowerId = followerId
		};

		this._applicationDbContext.Followers.Add(newFollower);
		await this._applicationDbContext.SaveChangesAsync();
	}

	public async Task UnfollowUserAsync(string userId, string followerId) {
		var follower = await this._applicationDbContext.Followers.FirstOrDefaultAsync(f => f.UserId.Equals(userId) && f.FollowerId.Equals(followerId));

		if (follower is null)
			return;

		this._applicationDbContext.Followers.Remove(follower);
		await this._applicationDbContext.SaveChangesAsync();
	}

	public async Task<bool> CheckFollowAsync(string userId, string followerId) {
		return (await this._applicationDbContext.Followers.FindAsync(userId, followerId)) is not null;
	}
}