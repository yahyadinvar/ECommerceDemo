using ECommerceDemo.API.Extensions;
using ECommerceDemo.Application.Features.Auth.Login;
using ECommerceDemo.Application.Features.Auth.Register;
using MediatR;

namespace ECommerceDemo.API.Endpoints;

public static class AuthEndpoints
{
    public static WebApplication MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/auth");

        group.MapPost("/register", async (RegisterCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return result.ToActionResult();
        }).AllowAnonymous();

        group.MapPost("/login", async (LoginCommand command, ISender sender) =>
        {
            var result = await sender.Send(command);
            return result.ToActionResult();
        }).AllowAnonymous();

        return app;
    }
}