using AutoMapper;
using ECommerceDemo.Application.Abstractions;
using ECommerceDemo.Application.Abstractions.Persistence;
using ECommerceDemo.Application.Common;
using System.Net;

namespace ECommerceDemo.Application.Features.Order.GetOrderByUserId;

public class GetOrderByUserIdQueryHandler : IQueryHandler<GetOrderByUserIdQuery, GetOrderByUserIdQueryResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderByUserIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }
    public async Task<Result<GetOrderByUserIdQueryResponse>> Handle(GetOrderByUserIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderByUserIdAsync(request.UserId);
        if (order is null)
            return Result<GetOrderByUserIdQueryResponse>.Failure("Bu userId'ye ait bir order sistemde mevcut değil.", HttpStatusCode.NotFound);

        // TODO: Redis'e yazılacak !
        return Result<GetOrderByUserIdQueryResponse>.Success(_mapper.Map<GetOrderByUserIdQueryResponse>(order));
    }
}
