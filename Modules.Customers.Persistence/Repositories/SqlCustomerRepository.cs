using Microsoft.EntityFrameworkCore;
using Modules.Customers.Application.Contracts;
using Modules.Customers.Domain.Entities;

namespace Modules.Customers.Persistence.Repositories
{
    internal class SqlCustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _dbContext;

        public SqlCustomerRepository(CustomerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            var customerModel = await _dbContext.Customers.SingleOrDefaultAsync(x => x.Id == id);

            if (customerModel is null)
            {
                return null;
            }

            return customerModel;
        }
    }
}
