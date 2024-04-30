﻿using Application.Contracts;
using Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.SQLRepositories;

internal class SqlCustomerRepository : ICustomerRepository
{
    private readonly SharedDbContext _dbContext;

    public SqlCustomerRepository(SharedDbContext dbContext)
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
