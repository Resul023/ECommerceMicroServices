using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Payment.API.Dtos;
using Shared.ControllerBases;
using Shared.Dtos;
using Shared.Messages;

namespace Payment.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentsController : CustomControllerBases
{
    private readonly ISendEndpointProvider _sendEndpointProvider;

    public PaymentsController(ISendEndpointProvider sendEndpointProvider)
    {
        this._sendEndpointProvider = sendEndpointProvider;
    }

    [HttpPost]
    public async Task<IActionResult> ReceivePayment(PaymentDto paymentDto)
    {

        var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order-service"));

        var createOrderMessageCommand = new CreateOrderMessageCommand();

        createOrderMessageCommand.BuyerId = paymentDto.Order.BuyerId;
        createOrderMessageCommand.Line = paymentDto.Order.Address.Line;
        createOrderMessageCommand.Street = paymentDto.Order.Address.Street;
        createOrderMessageCommand.ZipCode = paymentDto.Order.Address.ZipCode;
        createOrderMessageCommand.Province = paymentDto.Order.Address.Province;
        createOrderMessageCommand.District = paymentDto.Order.Address.District;

        paymentDto.Order.OrderItems.ForEach(x =>
        {
            createOrderMessageCommand.OrderItems.Add(new OrderItem
            {
                PictureUrl = x.PictureUrl,
                Price = x.Price,
                ProductId = x.ProductId,
                ProductName = x.ProductName
            });
        });

        await sendEndpoint.Send<CreateOrderMessageCommand>(createOrderMessageCommand);
        return CreateActionResultInstance(Shared.Dtos.Response<NoContent>.Success(200));
    }
}
