using System.Net;

namespace ECommerceDemo.Application.Common;
public class Result<T>
{
    public T? Data { get; set; }
    public List<string>? ErrorMessage { get; set; }
    public bool IsSuccess => ErrorMessage == null || ErrorMessage.Count == 0;
    public bool IsFail => !IsSuccess;
    public HttpStatusCode Status { get; set; }

    public static Result<T> Success(T data, HttpStatusCode status = HttpStatusCode.OK)
    {
        return new Result<T>()
        {
            Data = data,
            Status = status
        };
    }

    public static Result<T> Failure(List<string> errorMessages, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new Result<T>()
        {
            ErrorMessage = errorMessages,
            Status = status
        };
    }

    public static Result<T> Failure(string errorMessage, HttpStatusCode status = HttpStatusCode.BadRequest)
    {
        return new Result<T>()
        {
            ErrorMessage = [errorMessage],
            Status = status
        };
    }
}
