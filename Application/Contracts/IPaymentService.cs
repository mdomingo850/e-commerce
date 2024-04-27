using Ardalis.Result;

namespace Application.Contracts;

public interface IPaymentService
{
    Task<Result<bool>> ValidatePaymentOptionAsync();

    Task<Result> PayAsync();

    Task<Result> UndoPaymentAsync();
}
