using ECommerceDemo.Application.Abstractions;

namespace ECommerceDemo.Application.Features.Order.GetOrderByUserId;

public class GetOrderByUserIdQuery : IQuery<GetOrderByUserIdQueryResponse>
{
    public string UserId { get; set; }
}
