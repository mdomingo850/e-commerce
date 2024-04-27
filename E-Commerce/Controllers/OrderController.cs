using Application.Features.Orders.Commands.CreateOrder;
using Ardalis.Result;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
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
            var product = Product.Create(1, "Google Nest", new Money("$", 1), 5);
            var orderItem = new OrderItem(product, 3);
            var orderItems = new HashSet<Tuple<int, int>>() { new Tuple<int,int>(1, 5) };
            var command = new CreateOrderCommand(1, orderItems);

            await _mediator.Send(command);

            return Result.Success();
        }
    }
}
