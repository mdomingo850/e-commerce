using Application.Contracts;
using Ardalis.Result;
using Domain.Entities.Customers;

namespace Persistence.Repositories.BlankRepositories;

public class BlankCustomerRepository : ICustomerRepository
{


    public async Task<Customer?> GetByIdAsync(int id)
    {
        await Task.Delay(2000);

        return Customer.Create(id, "Clark", "Kent");
    }
}
