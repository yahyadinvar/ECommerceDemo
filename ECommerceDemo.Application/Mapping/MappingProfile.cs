using AutoMapper;
using ECommerceDemo.Application.Features.Order.CreateOrder;
using ECommerceDemo.Application.Features.Order.GetOrderByUserId;
using ECommerceDemo.Domain.Entities.Order;

namespace ECommerceDemo.Application.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<Order, CreateOrderCommandResponse>().ReverseMap();
        CreateMap<Order, GetOrdersByUserIdQueryResponse>().ReverseMap();
    }
}
