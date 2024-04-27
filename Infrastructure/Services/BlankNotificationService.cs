using Application.Contracts;
using Ardalis.Result;

namespace Infrastructure.Services;

public class BlankNotificationService : INotificationService
{
    public async Task<Result> SendAsync()
    {
        await Task.Delay(3000);

        return Result.Success();
    }
}
