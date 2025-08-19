using ECommerceDemo.Application.Abstractions;

namespace ECommerceDemo.Application.Features.Auth.Login;

public sealed record LoginCommand(string Email, string Password) : ICommand<LoginResponse>;
