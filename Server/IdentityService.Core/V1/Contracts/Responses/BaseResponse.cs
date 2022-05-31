using IdentityService.Core.V1.ValueObjects;

namespace IdentityService.Core.V1.Contracts.Responses;

public class BaseResponse<TBody> : BaseResponse {
	public new TBody? Content { get; set; }
}

public class BaseResponse {
	public bool Succeeded { get; set; } = false;
	public IEnumerable<Info> Messages { get; set; } = Enumerable.Empty<Info>();
	public IEnumerable<Info> Errors { get; set; } = Enumerable.Empty<Info>();
	public object? Content { get; set; } = null;
}