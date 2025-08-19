using ECommerceDemo.Application.Common;
using MediatR;

namespace ECommerceDemo.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}

