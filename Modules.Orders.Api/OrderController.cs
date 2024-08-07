using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Modules.Orders.Api.RequestPayload;
using Modules.Orders.Application.Features.CreateOrder;

namespace Modules.Orders.Api;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost(Name = "Order")]
    public async Task<IActionResult> Create(CreatePayload payload, CancellationToken cancellationToken)
    {
        var command = new CreateOrderCommand(payload.CustomerId, payload.OrderItems);

        var response = await _mediator.Send(command, cancellationToken);

        if (response.Status == ResultStatus.NotFound)
        {
            return NotFound(response);
        }
        else if (response.Status == ResultStatus.Conflict)
        {
            return Conflict(response);
        }
        else if (response.Status == ResultStatus.CriticalError)
        {
            return BadRequest(response.Errors[0]);
        }

        return Ok(response);
    }
}
