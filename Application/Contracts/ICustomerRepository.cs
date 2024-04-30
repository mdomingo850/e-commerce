using Domain.Entities.Customers;

namespace Application.Contracts;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id);
}
