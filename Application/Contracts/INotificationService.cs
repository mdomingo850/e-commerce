using Ardalis.Result;

namespace Application.Contracts;

public interface INotificationService
{
    Task<Result> SendAsync();
}
