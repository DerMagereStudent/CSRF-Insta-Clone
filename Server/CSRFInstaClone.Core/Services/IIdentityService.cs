using System.Collections.Generic;
using System.Threading.Tasks;

using CSRFInstaClone.Core.Dtos.External;

namespace CSRFInstaClone.Core.Services; 

public interface IIdentityService {
	Task SignUpAsync(string username, string email, string password);
	Task<UserDto> GetUserById(string userId);
	Task<List<UserDto>> GetUsersByNameOrEmailAsync(string usernameEmail);
}