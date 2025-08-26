using ECommerceDemo.API.Extensions;
using ECommerceDemo.Application.Features.Order.CreateOrder;
using ECommerceDemo.Application.Features.Order.GetOrderByUserId;
using MediatR;

namespace ECommerceDemo.API.Endpoints;

public static class OrderEndpoints
{
    public static WebApplication MapOrderEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/order");

        group.MapPost("/create", async (CreateOrderCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return result.ToActionResult();
        }).RequireAuthorization();

        group.MapGet("/get-orders-by-userId", async (string userId, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByUserIdQuery { UserId = userId });
            return result.ToActionResult();
        }).RequireAuthorization();

        return app;
    }
}