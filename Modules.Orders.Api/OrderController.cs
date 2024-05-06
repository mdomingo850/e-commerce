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
        var orderItems = new HashSet<Tuple<Guid, int>>() { new Tuple<Guid, int>(Guid.Parse("F73B3C15-51E8-4763-AE29-B4F6B7714A8D"), 5) };
        var command = new CreateOrderCommand(Guid.Parse("BB9E1061-BD8E-414D-BDEB-1C20904E3149"), orderItems);

        var response = await _mediator.Send(command);

        return response;
    }
}
