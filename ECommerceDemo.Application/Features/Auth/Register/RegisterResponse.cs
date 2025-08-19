namespace ECommerceDemo.Application.Features.Auth.Register;

public sealed record RegisterResponse(Guid Id, string FirstName, string LastName, string Email);