using IdentityService.Core.V1.Entities;

namespace IdentityService.Core.V1.Contracts.Responses.Authorization.Groups;

public class OrganizationalGroupResponse<TBody> : BaseResponse<TBody> where TBody : OrganizationalGroupResponse<TBody>.Body {
	public class Body {
		public OrganizationalGroup? Group { get; set; }
	}
}