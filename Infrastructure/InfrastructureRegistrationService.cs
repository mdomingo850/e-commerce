using Application.Contracts;
using Infrastructure.BackgroundJobs;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Infrastructure;

public static class InfrastructureRegistrationService
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentService, BlankPaymentService>();
        services.AddScoped<INotificationService, BlankNotificationService>();
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
