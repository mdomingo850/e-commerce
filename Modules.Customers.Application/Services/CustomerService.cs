using Modules.Customers.Application.Contracts;
using Modules.Customers.Domain.Entities;

namespace Modules.Customers.Application.Services;

public class CustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var customer = await _customerRepository.GetByIdAsync(id);

        return customer is null ? null : customer;
    }
}
