using Microsoft.Extensions.DependencyInjection;
using Modules.Orders.Infrastructure.BackgroundJobs;
using Quartz;

namespace Modules.Inventories.Infrastructure;

public static class InventoryInfrastructureRegistrationService
{
    public static IServiceCollection AddInventoryInfrastructureServices(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessInventoryInboxMessagesJob));
            configure.AddJob<ProcessInventoryInboxMessagesJob>(jobKey)
            .AddTrigger(trigger => trigger.ForJob(jobKey)
                .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(10).RepeatForever()));

            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();

        return services;
    }
}
