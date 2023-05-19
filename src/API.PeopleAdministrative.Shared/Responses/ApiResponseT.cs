namespace API.PeopleAdministrative.Shared.Responses;

public sealed class ApiResponse<T> : ApiResponse
{
    public T Result { get; private set; }

    public static ApiResponse<T> Ok(T result)
    {
        return new ApiResponse<T>
        {
            Success = true,
            StatusCode = 200,
            Result = result
        };
    }
}
