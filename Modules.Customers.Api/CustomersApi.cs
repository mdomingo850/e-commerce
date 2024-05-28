using Ardalis.Result;
using Modules.Customers.Application.Contracts;
using Modules.Customers.Application.Services;

namespace Modules.Customers.Api;

public sealed class CustomersApi(CustomerService customerService) : ICustomersApi
{
    public async Task<CustomerResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var customer = await customerService.GetByIdAsync(id, cancellationToken);

        return customer is null ? null : new CustomerResponse(customer.Id, $"{customer.FirstName} {customer.LastName}");
    }
}
