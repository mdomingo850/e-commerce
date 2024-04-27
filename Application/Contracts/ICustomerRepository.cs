using Domain.Entities.Customers;

namespace Application.Contracts;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(int id);
}
