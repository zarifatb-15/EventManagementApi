using WebApplicationApi.Entity; 

namespace WebApplicationApi.Helpers;

public class ResponseModelHelper
{
    public static ResponseModel<T> CreateSuccessResponse<T>(T data, int statusCode = 200)
    {
        return new ResponseModel<T>
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Errors = null,
            Data = data
        };
    }

    public static ResponseModel<T> CreateBadRequestResponse<T>(string error)
    {
        return new ResponseModel<T>
        {
            IsSuccess = false,
            StatusCode = 400,
            Errors = new List<string> { error },
            Data = default
        };
    }

    public static ResponseModel<T> CreateNotFoundResponse<T>(string error)
    {
        return new ResponseModel<T>
        {
            IsSuccess = false,
            StatusCode = 404,
            Errors = new List<string> { error },
            Data = default
        };
    }
    public static ResponseModel<T> CreateConflictResponse<T>(string error)
    {
        return new ResponseModel<T>
        {
            IsSuccess = false,
            StatusCode = 409,
            Errors = new List<string> { error },
            Data = default
        };
    }
    
    public static ResponseModel<T> CreateErrorResponse<T>(List<string> errors)
    {
        return new ResponseModel<T>
        {
            IsSuccess = false,
            StatusCode = 500,
            Errors = errors,
            Data = default
        };
    }
}