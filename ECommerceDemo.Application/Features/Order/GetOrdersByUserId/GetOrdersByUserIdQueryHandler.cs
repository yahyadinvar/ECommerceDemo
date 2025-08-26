using AutoMapper;
using ECommerceDemo.Application.Abstractions;
using ECommerceDemo.Application.Abstractions.Persistence;
using ECommerceDemo.Application.Common;
using System.Net;

namespace ECommerceDemo.Application.Features.Order.GetOrderByUserId;

public class GetOrderByUserIdQueryHandler : IQueryHandler<GetOrdersByUserIdQuery, List<GetOrdersByUserIdQueryResponse>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ICacheRepository _cacheRepository;

    public GetOrderByUserIdQueryHandler(IOrderRepository orderRepository, IMapper mapper, ICacheRepository cacheRepository)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _cacheRepository = cacheRepository;
    }
    public async Task<Result<List<GetOrdersByUserIdQueryResponse>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        string cacheKey = $"orders:{request.UserId}";

        //  Cache kontrol
        var cachedOrders = await _cacheRepository.GetAsync<Result<List<GetOrdersByUserIdQueryResponse>>>(cacheKey);
        if (cachedOrders != null) return cachedOrders;

        var orders = await _orderRepository.GetOrdersByUserIdAsync(request.UserId);
        if (!orders.Any())
            return Result<List<GetOrdersByUserIdQueryResponse>>.Failure("Bu userId'ye ait bir order sistemde mevcut değil.", HttpStatusCode.NotFound);

        var result = Result<List<GetOrdersByUserIdQueryResponse>>.Success(
            _mapper.Map<List<GetOrdersByUserIdQueryResponse>>(orders));

        // Cache’e yaz 
        await _cacheRepository.SetAsync(cacheKey, result, TimeSpan.FromMinutes(2));

        return result;
    }
}
