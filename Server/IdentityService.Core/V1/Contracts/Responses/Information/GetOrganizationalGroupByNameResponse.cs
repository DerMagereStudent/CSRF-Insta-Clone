using IdentityService.Core.V1.Contracts.Responses.Authorization.Groups;
using IdentityService.Core.V1.ValueObjects;

namespace IdentityService.Core.V1.Contracts.Responses.Information; 

public class GetOrganizationalGroupByNameResponse : OrganizationalGroupResponse<GetOrganizationalGroupByNameResponse.Body> {
	public class Body : OrganizationalGroupResponse<Body>.Body {}
	
	public static class Message {
		public static readonly Info GroupReturnedSuccessfully = new Info {
			Code = nameof(GroupReturnedSuccessfully),
			Description = "A group with this name was found and returned successfully"
		};
	}

	public static class Error {
		public static Info OrganizationalUnitDoesNotExist = new() {
			Code = nameof(OrganizationalUnitDoesNotExist),
			Description = "The organizational unit for the specified name does not exist"
		};
	}
}