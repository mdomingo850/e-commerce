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
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }