using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public async Task<Result> Create()
    {
        var orderItems = new HashSet<Tuple<Guid, int>>() { new Tuple<Guid, int>(Guid.Parse("5F341EC0-38F2-4A3E-84D7-1EB51885A95D"), 5) };
        var command = new CreateOrderCommand(Guid.Parse("AC8572BA-8742-43BE-AC63-FD69654A7188"), orderItems);

        var response = await _mediator.Send(command);

        return response;
    }
}
