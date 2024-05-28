using Ardalis.Result;
using Modules.Notifications.Application.Contracts;

namespace Infrastructure.Services;

public class BlankNotificationService : INotificationService
{
    public async Task<Result> SendAsync()
    {
        await Task.Delay(1000);

        return Result.Success();
    }
}
