using ECommerceDemo.Application.Abstractions;
using ECommerceDemo.Application.Abstractions.Authentication;
using ECommerceDemo.Application.Abstractions.Persistence;
using ECommerceDemo.Application.Common;
using ECommerceDemo.Application.Configurations;
using ECommerceDemo.Domain.Entities.User;
using Microsoft.Extensions.Options;
using System.Net;

namespace ECommerceDemo.Application.Features.Auth.Login;

public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly JwtSettings _jwtOptions;

    public LoginCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IOptions<JwtSettings> jwtOptions)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetUserByEmailAsync(request.Email);
        if (user is null)
            return Result<LoginResponse>.Failure("Bu mail adresine ait bir kullanıcı mevcut değil.", HttpStatusCode.NotFound);

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Result<LoginResponse>.Failure("Girmiş olduğunuz şifre yanlış.");

        string? token = _jwtTokenGenerator.GenerateToken(user);

        LoginResponse loginResponse = new LoginResponse(user!.Id, user.FirstName, user.LastName, user.Email,
                                             token, DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryMinutes), user.Role);

        return Result<LoginResponse>.Success(loginResponse);
    }
}
