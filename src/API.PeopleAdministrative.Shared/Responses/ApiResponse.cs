using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.PeopleAdministrative.Shared.Responses;

/// <summary>
/// Responsável pela padronização das respostas da API.
/// </summary>
public class ApiResponse
{
    public bool Success { get; protected set; }
    public int StatusCode { get; protected set; }
    public IEnumerable<ApiError> Errors { get; protected set; } = Enumerable.Empty<ApiError>();

    public static ApiResponse Ok()
    {
        return new ApiResponse
        {
            Success = true,
            StatusCode = 200
        };
    }

    public static ApiResponse BadRequest()
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 400
        };
    }

    public static ApiResponse BadRequest(string errorMessage)
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 400,
            Errors = CreateApiErrors(errorMessage)
        };
    }

    public static ApiResponse BadRequest(IEnumerable<ApiError> errors)
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 400,
            Errors = errors
        };
    }

    public static ApiResponse Unauthorized()
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 401
        };
    }

    public static ApiResponse Anauthorized(string errorMessage)
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 401,
            Errors = CreateApiErrors(errorMessage)
        };
    }

    public static ApiResponse Unauthorized(IEnumerable<ApiError> errors)
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 401,
            Errors = errors
        };
    }

    public static ApiResponse Forbidden()
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 500
        };
    }

    public static ApiResponse Forbidden(string errorMessage)
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 403,
            Errors = CreateApiErrors(errorMessage)
        };
    }

    public static ApiResponse Forbidden(IEnumerable<ApiError> errors)
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 403,
            Errors = errors
        };
    }

    public static ApiResponse NotFound()
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 404
        };
    }

    public static ApiResponse NotFound(string errorMessage)
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 404,
            Errors = CreateApiErrors(errorMessage)
        };
    }

    public static ApiResponse NotFound(IEnumerable<ApiError> errors)
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 404,
            Errors = errors
        };
    }

    public static ApiResponse InternalServerError(string errorMessage) 
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 500,
            Errors = CreateApiErrors(errorMessage)
        };
    }

    public static ApiResponse InternalServerError(IEnumerable<ApiError> errors)
    {
        return new ApiResponse
        {
            Success = false,
            StatusCode = 500,
            Errors = errors
        };
    }

    public static IEnumerable<ApiError> CreateApiErrors(string errorMessage)
    {
        return new ApiError[1]
        {
            new ApiError(errorMessage)
        };
    }
}
