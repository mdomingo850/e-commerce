using Application.Contracts;
using Ardalis.Result;

namespace Infrastructure.Services;

public class BlankPaymentService : IPaymentService
{
    private readonly Random _random = new Random();
    private readonly float _failProbability = 0.01f;

    public async Task<Result> PayAsync()
    {
        await Task.Delay(2000);

        if (_random.NextDouble() < _failProbability)
        {
            return Result.Conflict();
        }

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

        if (_random.NextDouble() < _failProbability)
        {
            return Result.Conflict();
        }

        return Result.Success(true);
    }
}
