using ECommerceDemo.Application.Abstractions;

namespace ECommerceDemo.Application.Features.Auth.Register;

public sealed record RegisterCommand(string FirstName, string LastName, string Email, string Password) : ICommand<RegisterResponse>;

