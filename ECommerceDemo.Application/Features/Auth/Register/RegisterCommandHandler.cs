using ECommerceDemo.Application.Abstractions;
using ECommerceDemo.Application.Abstractions.Persistence;
using ECommerceDemo.Application.Common;
using ECommerceDemo.Domain.Entities.User;
using System.Net;

namespace ECommerceDemo.Application.Features.Auth.Register;

public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand, RegisterResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        User existingUser = await _userRepository.GetUserByEmailAsync(request.Email);
        if (existingUser is not null)
            return Result<RegisterResponse>.Failure("Bu e-posta adresi zaten kullanımda.", HttpStatusCode.Conflict);

        string? passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        User user = new User(request.FirstName, request.LastName, request.Email, passwordHash);

        await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        return Result<RegisterResponse>.Success(new RegisterResponse(user.Id, user.FirstName, user.LastName, user.Email));
    }
}