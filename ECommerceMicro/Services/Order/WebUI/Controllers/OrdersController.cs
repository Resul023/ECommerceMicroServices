using MediatR;
using Shared.Services;
using Application.Queries;
using Application.Commands;
using Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : CustomControllerBases
{
    private readonly IMediator _mediator;
    private readonly ISharedIdentityService _sharedIdentityService;

    public OrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
    {
        this._mediator = mediator;
        this._sharedIdentityService = sharedIdentityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _sharedIdentityService.GetUserId });

        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand createOrderCommand)
    {
        var response = await _mediator.Send(createOrderCommand);

        return CreateActionResultInstance(response);
    }
}
