using ECommerceDemo.Application.Common;
using MediatR;

namespace ECommerceDemo.Application.Abstractions;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>
{
}