//using Application.Contracts;
//using AutoMapper;
//using Domain.Entities.Customers;
//using Domain.Entities.Orders;
//using Domain.Entities.Products;
//using Domain.ValueObjects;
//using Microsoft.EntityFrameworkCore;

//namespace Persistence.Repositories.SQLRepositories;

//internal class SqlOrderRespository : IOrderRepository
//{
//    private readonly SharedDbContext _dbContext;
//    private readonly IMapper _mapper;

//    public SqlOrderRespository(
//        SharedDbContext dbContext, 
//        IMapper mapper)
//    {
//        _dbContext = dbContext;
//        _mapper = mapper;
//    }

//    public async Task AddAsync(Order order)
//    {
//        var orderModel = _mapper.Map<Models.Order>(order);
//        _dbContext.Orders.Add(orderModel);
//        await _dbContext.SaveChangesAsync();
//    }

//    public async Task<Order?> GetByIdAsync(int id)
//    {
//        var orderModel = await _dbContext.Orders.SingleOrDefaultAsync(x => x.Id == id);

//        if (orderModel == null)
//        {
//            return null;
//        }

//        var customer = new Customer(orderModel.Customer.Id, orderModel.Customer.FirstName, orderModel.Customer.LastName);
//        var orderItemModel = orderModel.OrderItems.First();
//        var productModel = orderItemModel.Product;
//        var product = new Product(productModel.Id, productModel.Name, new Money(productModel.Currency, productModel.Price), productModel.Quantity);
//        var orderItem = new OrderItem(product, orderItemModel.Quanitity);

//        return Order.Create(customer, new HashSet<OrderItem>() { orderItem }, orderModel.OrderDate, orderModel.OrderStatus, orderModel.Id);
//    }

//    public async Task UpdateAsync(Order order)
//    {
//        _dbContext.Update(order);
//        await _dbContext.SaveChangesAsync();
//    }
//}
