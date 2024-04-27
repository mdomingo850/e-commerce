using Application.Contracts;
using Ardalis.Result;

namespace Infrastructure.Services;

public class BlankPaymentService : IPaymentService
{
    public async Task<Result> PayAsync()
    {
        await Task.Delay(2000);

        return Result.Success();
    }

    public async Task<Result> UndoPaymentAsync()
    {
        await Task.Delay(2000);

        return Result.Success();
    }

    public async Task<Result<bool>> ValidatePaymentOptionAsync()
    {
        await Task.Delay(2000);

        return Result.Success(true);
    }
}
