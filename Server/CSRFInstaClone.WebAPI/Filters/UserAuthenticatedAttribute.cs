using System;
using System.Net.Http;
using System.Threading.Tasks;

using CSRFInstaClone.Core.Contracts.External.Requests.IdentityService;
using CSRFInstaClone.Core.Contracts.External.Responses.IdentityService;
using CSRFInstaClone.Core.Options;
using CSRFInstaClone.Infrastructure.Extensions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace CSRFInstaClone.WebAPI.Filters; 

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class UserAuthenticatedAttribute : Attribute, IAsyncAuthorizationFilter {
	public async Task OnAuthorizationAsync(AuthorizationFilterContext context) {
		if (!context.HttpContext.Request.Cookies.ContainsKey(HeaderNames.Authorization)) {
			context.Result = new UnauthorizedResult();
			return;
		}

		var token = context.HttpContext.Request.Cookies[HeaderNames.Authorization]!;
		var gatewayOptions = context.HttpContext.RequestServices.GetRequiredService<IOptions<GatewayOptions>>().Value;
		
		Console.WriteLine(token);

		using var httpClient = new HttpClient();
		var response = await httpClient.SendPostAsync<AuthorizeRequest, AuthorizeResponse>(
			gatewayOptions.Routes.IdentityServiceAuthorizeUser,
			new AuthorizeRequest { Token = token }
		);

		if (response is null || !response.Succeeded || response.Content is null || !response.Content.Authorized)
			context.Result = new UnauthorizedResult();
	}
}