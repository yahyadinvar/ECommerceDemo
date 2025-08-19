using ECommerceDemo.Application.Common;
using MediatR;

namespace ECommerceDemo.Application.Abstractions;

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>> where TCommand : ICommand<TResponse>
{
}

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result<Unit>> where TCommand : ICommand
{
}

