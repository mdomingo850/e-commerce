using Modules.Customers.Domain.Entities;

namespace Modules.Customers.Application.Contracts;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id);
}
