using Ardalis.Result;

namespace Modules.Customers.Api;

public interface ICustomersApi
{
    Task<CustomerResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
