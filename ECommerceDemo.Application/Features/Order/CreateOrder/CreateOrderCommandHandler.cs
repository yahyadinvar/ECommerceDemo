using AutoMapper;
using ECommerceDemo.Application.Abstractions;
using ECommerceDemo.Application.Abstractions.Persistence;
using ECommerceDemo.Application.Common;

namespace ECommerceDemo.Application.Features.Order.CreateOrder;

public class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, CreateOrderCommandResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheRepository _cacheRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IUnitOfWork unitOfWork, ICacheRepository cacheRepository)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _cacheRepository = cacheRepository;
    }
    public async Task<Result<CreateOrderCommandResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Order.Order order = new();
        order.Added(request.UserId, request.ProductId, request.Quantity, request.PaymentMethod);

        await _orderRepository.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();

        // Cache invalidate
        await _cacheRepository.RemoveAsync($"orders:{request.UserId}");

        return Result<CreateOrderCommandResponse>.Success(_mapper.Map<CreateOrderCommandResponse>(order));
    }
}
