using Ardalis.Result;

namespace Modules.Payments.Api;

public interface IPaymentsApi
{
    Task<Result<bool>> ValidatePaymentOptionAsync();

    Task<Result> PayAsync();

    Task<Result> UndoPaymentAsync();
}
