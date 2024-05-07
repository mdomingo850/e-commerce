using Microsoft.Extensions.DependencyInjection;
using Modules.Orders.Infrastructure.BackgroundJobs;
using Quartz;

namespace Modules.Orders.Infrastructure;

public static class InfrastructureRegistrationService
{
    public static IServiceCollection AddOrderInfrastructureServices(this IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
            configure.AddJob<ProcessOutboxMessagesJob>(jobKey)
            .AddTrigger(trigger => trigger.ForJob(jobKey)
                .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(10).RepeatForever()));

            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();

        return services;
    }
}
