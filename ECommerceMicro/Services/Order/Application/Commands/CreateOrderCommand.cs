using MediatR;
using Shared.Dtos;
using Application.Dtos;

namespace Application.Commands;
public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
{
    public string BuyerId { get; set; }

    public List<OrderItemDto> OrderItems { get; set; }

    public AddressDto Address { get; set; }
}
