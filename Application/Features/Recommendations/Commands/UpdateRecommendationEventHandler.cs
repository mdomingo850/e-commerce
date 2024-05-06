//using Application.Contracts;
//using Application.Features.Notifications.Commands;
//using Domain.Entities.Orders;
//using MediatR;
//using Microsoft.Extensions.Logging;

//namespace Application.Features.Recommendations.Commands;

//internal class UpdateRecommendationEventHandler : INotificationHandler<OrderCreatedDomainEvent>
//{
//    private readonly ILogger<UpdateRecommendationEventHandler> _logger;

//    public UpdateRecommendationEventHandler( ILogger<UpdateRecommendationEventHandler> logger)
//    {
//        _logger = logger;
//    }

//    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
//    {
//        _logger.LogInformation("Update customer's recommendation started");
//        await Task.Delay(2000);
//        _logger.LogInformation("Update customer's recommendationcompleted");
//    }
//}
