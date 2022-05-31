using System.Security.Claims;

using IdentityService.Core.V1.Options;

namespace IdentityService.Core.V1.Services; 

public interface IJwtIssuingService {
	string CreateToken(JwtIssuingOptions options, IEnumerable<Claim> claims);
	IEnumerable<Claim>? GetClaimsFromToken(string token);
	void ValidateToken(string token);
	bool IsValidToken(string token);
}