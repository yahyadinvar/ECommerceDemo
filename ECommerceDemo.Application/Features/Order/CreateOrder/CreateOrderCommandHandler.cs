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

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<CreateOrderCommandResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Order.Order order = new();
        order.Added(request.UserId, request.ProductId, request.Quantity, request.PaymentMethod);

        await _orderRepository.AddAsync(order);
        await _unitOfWork.SaveChangesAsync();

        return Result<CreateOrderCommandResponse>.Success(_mapper.Map<CreateOrderCommandResponse>(order));
    }
}
