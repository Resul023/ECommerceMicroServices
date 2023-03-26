using MediatR;
using Shared.Dtos;
using Application.Dtos;

namespace Application.Queries;
public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
{
    public string UserId { get; set; }
}
