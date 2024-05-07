using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories.SQLRepositories;

namespace Persistence;

public static class PersistenceRegistrationServices
{
    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        return services;
    }
}
