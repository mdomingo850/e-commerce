using Ardalis.Result;
using Modules.Payments.Application.Contracts;

namespace Modules.Payments.Infrastructure;

public class PaymentService : IPaymentService
{
    public async Task<Result> PayAsync()
    {
        await Task.Delay(3000);

        return Result.Success();
    }

    public async Task<Result> UndoPaymentAsync()
    {
        await Task.Delay(3000);

        return Result.Success();
    }

    public async Task<Result<bool>> ValidatePaymentOptionAsync()
    {
        await Task.Delay(500);

        return Result.Success(true);
    }
}
