using AutoMapper;
using Application.Dtos;
using Domain.OrderAggregate;

namespace Application.Mapping;
public class CustomMapping:Profile
{
    public CustomMapping()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<Address, AddressDto>().ReverseMap();
    }
}
