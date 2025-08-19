using ECommerceDemo.Application.Common;
using System.Net;

namespace ECommerceDemo.API.Extensions;
public static class ResultExtensions
{
    public static IResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return Results.Ok(result);

        return (result.Status) switch
        {
            HttpStatusCode.NotFound => Results.NotFound(result),
            HttpStatusCode.Conflict => Results.Conflict(result),
            HttpStatusCode.BadRequest => Results.BadRequest(result),
            HttpStatusCode.Unauthorized => Results.Unauthorized(),
            HttpStatusCode.InternalServerError => Results.Json(result, statusCode: StatusCodes.Status500InternalServerError),
            _ => Results.Json(result, statusCode: (int)result.Status)
        };
    }
}