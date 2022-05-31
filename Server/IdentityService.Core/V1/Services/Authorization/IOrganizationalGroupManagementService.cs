using IdentityService.Core.V1.Contracts.Requests.Authorization.Groups;
using IdentityService.Core.V1.Contracts.Responses.Authorization.Groups;

namespace IdentityService.Core.V1.Services.Authorization; 

public interface IOrganizationalGroupManagementService {
	Task<AddOrganizationalGroupResponse> AddOrganizationalGroupAsync(AddOrganizationalGroupRequest request);
	Task<RemoveOrganizationalGroupResponse> RemoveOrganizationalGroupAsync(RemoveOrganizationalGroupRequest request);
	Task<UpdateOrganizationalGroupResponse> UpdateOrganizationalGroupAsync(UpdateOrganizationalGroupRequest request);
}