using ECommerceDemo.Application.Common;
using MediatR;

namespace ECommerceDemo.Application.Abstractions;

public interface ICommand : IRequest<Result<Unit>>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}