using WebApplicationApi.Entity;

namespace WebApplicationApi.Helpers;

public class ResponseModelHelper
{
    public static ResponseModel<T> CreateSuccessResponse<T>(T data)
    {
        return new ResponseModel<T>
        {
            IsSuccess = true,
            Data = data,
            Errors = null
        };
    }

    public static ResponseModel<T> CreateErrorResponse<T>(List<string> errors)
    {
        return new ResponseModel<T>
        {
            IsSuccess = false,
            Data = default,
            Errors = errors
        };
    }
}