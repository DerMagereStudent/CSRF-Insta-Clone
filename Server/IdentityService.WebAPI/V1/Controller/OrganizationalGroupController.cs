using IdentityService.Core.V1.Contracts.Requests.Authorization.Groups;
using IdentityService.Core.V1.Contracts.Responses.Authorization.Groups;
using IdentityService.Core.V1.Services.Authorization;
using IdentityService.WebAPI.V1.Extensions;

using Microsoft.AspNetCore.Mvc;

namespace IdentityService.WebAPI.V1.Controller; 

[ApiController]
[Route("v1/authorization/group")]
public class OrganizationalGroupController : ControllerBase {
	private readonly IOrganizationalGroupManagementService _organizationalGroupManagementService;
	private readonly ILogger<OrganizationalGroupController> _logger;

	public OrganizationalGroupController(IOrganizationalGroupManagementService organizationalGroupManagementService, ILogger<OrganizationalGroupController> logger) {
		this._organizationalGroupManagementService = organizationalGroupManagementService;
		this._logger = logger;
	}

	/// <summary>
	/// Creates an organizational group.
	/// </summary>
	[HttpPost]
	[ProducesResponseType(typeof(AddOrganizationalGroupResponse), 200)]
	public async Task<IActionResult> AddOrganizationalGroupAsync([FromBody] AddOrganizationalGroupRequest request) {
		if (!this.HasValidModelState(out AddOrganizationalGroupResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._organizationalGroupManagementService.AddOrganizationalGroupAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.AddOrganizationalGroupAsync)} threw an exception");
			return this.InternalServerError<AddOrganizationalGroupResponse>(e);
		}
	}

	/// <summary>
	/// Deletes an organizational group of the passed id.
	/// </summary>
	[HttpDelete]
	[ProducesResponseType(typeof(RemoveOrganizationalGroupResponse), 200)]
	public async Task<IActionResult> RemoveOrganizationalGroupAsync([FromBody] RemoveOrganizationalGroupRequest request) {
		if (!this.HasValidModelState(out RemoveOrganizationalGroupResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._organizationalGroupManagementService.RemoveOrganizationalGroupAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.RemoveOrganizationalGroupAsync)} threw an exception");
			return this.InternalServerError<RemoveOrganizationalGroupResponse>(e);
		}
	}

	/// <summary>
	/// Edits an organizational group.
	/// </summary>
	[HttpPut]
	[Route("update")]
	[ProducesResponseType(typeof(UpdateOrganizationalGroupResponse), 200)]
	public async Task<IActionResult> UpdateOrganizationalGroupAsync([FromBody] UpdateOrganizationalGroupRequest request) {
		if (!this.HasValidModelState(out UpdateOrganizationalGroupResponse? response))
			return this.BadRequest(response);
		
		try {
			return this.Ok(await this._organizationalGroupManagementService.UpdateOrganizationalGroupAsync(request));
		}
		catch (Exception e) {
			this._logger.LogError(e, $"{nameof(this.UpdateOrganizationalGroupAsync)} threw an exception");
			return this.InternalServerError<UpdateOrganizationalGroupResponse>(e);
		}
	}
}