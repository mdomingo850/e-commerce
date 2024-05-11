
using Serilog;
using Modules.Customers.Api;
using Modules.Inventories.Api;
using Modules.Orders.Api;
using Modules.Payments.Api;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services
    .AddCustomerModuleServices()
    .AddInventoryModuleServices()
    .AddOrderModuleServices()
    .AddPaymentModuleServices()
    .AddInfrastructureServices(builder.Configuration);

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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
