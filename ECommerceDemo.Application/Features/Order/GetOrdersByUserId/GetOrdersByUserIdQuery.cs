using ECommerceDemo.Application.Abstractions;

namespace ECommerceDemo.Application.Features.Order.GetOrderByUserId;

public class GetOrdersByUserIdQuery : IQuery<List<GetOrdersByUserIdQueryResponse>>
{
    public string UserId { get; set; }
}
