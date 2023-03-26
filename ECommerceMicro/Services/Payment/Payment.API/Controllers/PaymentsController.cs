using Microsoft.AspNetCore.Mvc;
using Payment.API.Dtos;
using Shared.ControllerBases;
using Shared.Dtos;

namespace Payment.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PaymentsController : CustomControllerBases
{
    [HttpPost]
    public async Task<IActionResult> ReceivePayment()
    {
        return CreateActionResultInstance(Response<NoContent>.Success(200));
    }
}
