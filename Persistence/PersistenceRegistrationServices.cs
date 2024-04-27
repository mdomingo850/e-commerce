using Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories.BlankRepositories;
using Persistence.Repositories.SQLRepositories;

namespace Persistence;

public static class PersistenceRegistrationServices
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        services.AddDbContext<SharedDbContext>();
        services.AddScoped<ICustomerRepository, SqlCustomerRepository>();
        services.AddScoped<IInventoryRepository, BlankInventoryRepository>();
        services.AddScoped<IOrderRepository, BlankOrderRepository>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        return services;
    }
}
