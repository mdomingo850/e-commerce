using Serilog;
using Modules.Customers.Api;
using Modules.Inventories.Api;
using Modules.Orders.Api;
using Modules.Payments.Api;
using MassTransit;
using Modules.Notifications.Api.MessageConsumers;
using Modules.Notifications.Api;
using Modules.Payments.Api.MessageHandlers;
using Modules.Inventories.Api.MessageConsumers;
using Modules.Orders.Api.MessageConsumers;
using Modules.Orders.Api.Sagas;
using Modules.Orders.Persistence;
using Modules.Orders.Domain.Entities;
using E_Commerce;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Http.Timeouts;
using E_Commerce.Middlewares;
using SharedKernel.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services
    .AddCustomerModuleServices()
    .AddInventoryModuleServices()
    .AddOrderModuleServices()
    .AddPaymentModuleServices()
    .AddNotificationsModuleServices();

builder.Services.AddMassTransit((busConfigurator) =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<SendOrderConfirmationConsumer>();
    busConfigurator.AddConsumer<PayOrderConsumer>();
    busConfigurator.AddConsumer<ReserveProductConsumer>();
    busConfigurator.AddConsumer<OrderProcessingCompletedConsumer>();
    busConfigurator.AddConsumer<ReverseOrderPaymentConsumer>();

    busConfigurator.AddSagaStateMachine<OrderProcessingSaga, OrderProcessingSagaData>()
        .EntityFrameworkRepository(r =>
        {
            r.ExistingDbContext<OrderDbContext>();
        });
    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(builder.Configuration["MessageBroker:Host"], h =>
        {
            h.Username(builder.Configuration["MessageBroker:Username"]!);
            h.Password(builder.Configuration["MessageBroker:Password"]!);
        });

        configurator.ConfigureEndpoints(context);
    });
});

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRequestTimeouts(options => {
    options.DefaultPolicy =
        new RequestTimeoutPolicy { Timeout = TimeSpan.FromMilliseconds(600) };
});
builder.Services.AddSwaggerGen();

var rabbitMQConnectionString = builder.Configuration["MessageBroker:Host"];

builder.Services.AddHealthChecks()
    .AddRabbitMQ(rabbitConnectionString: rabbitMQConnectionString);

var apiName = Environment.GetEnvironmentVariable("API_NAME");

builder.Services.AddScoped<IEnvironmentVariables>(x => new EnvironmentVariables(apiName));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseAuthorization();
app.UseRequestTimeouts();
app.UseMiddleware<LoggingMiddleware>();
app.MapControllers();

app.Run();

public partial class Program { }