using Ardalis.Result;
using Modules.Payments.Application.Contracts;

namespace Modules.Payments.Api;

internal sealed class PaymentsApi(IPaymentService paymentService) : IPaymentsApi
{
    public async Task<Result> PayAsync()
    {
        await paymentService.PayAsync();

        return Result.Success();
    }

    public async Task<Result> UndoPaymentAsync()
    {
        await paymentService.UndoPaymentAsync();

        return Result.Success();
    }

    public async Task<Result<bool>> ValidatePaymentOptionAsync()
    {
        var validatePaymentOptionsResult =  await paymentService.ValidatePaymentOptionAsync();

        return Result.Success(validatePaymentOptionsResult.Value);
    }
}
