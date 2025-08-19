namespace ECommerceDemo.Application.Features.Auth.Login;
public sealed record LoginResponse(Guid Id, string FirstName, string LastName, string Email, string Token, DateTime Expiration, string Role);