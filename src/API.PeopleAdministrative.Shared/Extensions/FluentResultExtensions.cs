using API.PeopleAdministrative.Shared.Errors;
using API.PeopleAdministrative.Shared.Responses;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace API.PeopleAdministrative.Shared.Extensions;

public static class FluentResultExtensions
{
    private static readonly OkObjectResult EmptyOkObject = new OkObjectResult(ApiResponse.Ok());

    public static IActionResult ToActionResult(this Result result)
    {
        IActionResult result2;
        if (!result.IsFailed)
        {
            IActionResult emptyOkResult = EmptyOkObject;
            result2 = emptyOkResult;
        }
        else
        {
            result2 = result.ToHttpNonSuccessResult();
        }
        
        return result2;
    }

    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        IActionResult result2;
        if (!result.IsFailed)
        {
            IActionResult emptyOkResult = new OkObjectResult(ApiResponse<T>.Ok(result.Value));
            result2 = emptyOkResult;
        }
        else
        {
            result2 = result.ToHttpNonSuccessResult();
        }

        return result2;
    }

    private static IActionResult ToHttpNonSuccessResult(this ResultBase result)
    {
        IEnumerable<ApiError> errors = result.Errors.ToApiErrors();
        ApiResponse apiResponse = (result.HasError<ValidationError>() || result.HasError<BusinessError>()) ? ApiResponse.BadRequest(errors) : (result.HasError<UnauthorizedError>() ? ApiResponse.Unauthorized(errors) : (result.HasError<ForbiddenError>() ? ApiResponse.Forbidden(errors) : ((!result.HasError<NotFoundError>()) ? ApiResponse.InternalServerError(errors) : ApiResponse.NotFound(errors))));
        return new ObjectResult(apiResponse)
        {
            StatusCode = apiResponse.StatusCode
        };
    }

    private static IEnumerable<ApiError> ToApiErrors(this IEnumerable<IError> errors)
    {
        return (from message in errors.Select((IError error) => error.Message).Distinct()
                orderby message
                select new ApiError(message)).ToList();
    }
}
