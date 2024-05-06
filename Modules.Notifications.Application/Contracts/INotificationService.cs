using Ardalis.Result;

namespace Modules.Notifications.Application.Contracts;

public interface INotificationService
{
    Task<Result> SendAsync();
}
