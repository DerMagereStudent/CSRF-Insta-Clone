using System;
using System.Linq;

using CSRFInstaClone.Core.Contracts.Responses;
using CSRFInstaClone.Core.Exceptions;
using CSRFInstaClone.Core.ValueObjects;

using Microsoft.AspNetCore.Mvc;

namespace CSRFInstaClone.WebAPI.Extensions; 

public static class ControllerExtensions {
	public static bool HasValidModelState<TResponse>(this ControllerBase controller, out TResponse? response) where TResponse : BaseResponse {
		if (controller.ModelState.IsValid) {
			response = null;
			return true;
		}

		response = Activator.CreateInstance<TResponse>();
		response.Errors = controller.ModelState.Values.SelectMany(value => value.Errors).Select(error => new Info {
			Code = error.Exception?.GetType().Name ?? "",
			Description = error.ErrorMessage
		});

		return false;
	}
	
	public static IActionResult InternalServerErrorResponse<TResponse>(this ControllerBase controller) where TResponse : BaseResponse {
		var response = Activator.CreateInstance<TResponse>();
		response.Errors = new[] {
			new Info {
				Code = "InternalServerError",
				Description = "An internal server error occured during the request"
			}
		};
		
		return controller.StatusCode(500, response);
	}
	public static IActionResult InfoExceptionResponse<TResponse>(this ControllerBase controller, InfoException e) where TResponse : BaseResponse {
		var response = Activator.CreateInstance<TResponse>();
		response.Errors = e.Errors;
		return controller.Ok(response);
	}
}