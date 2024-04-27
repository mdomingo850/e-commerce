using AutoMapper;
using Domain.Entities.Customers;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Domain.ValueObjects;

namespace Persistence.MappingProfiles;

internal class SqlMappingProfiles : Profile
{
    public SqlMappingProfiles()
    {
        CreateMap<Models.Order, Order>()
            .ReverseMap();
        CreateMap<Models.Customer, Customer>()
            .ReverseMap();
        CreateMap<Models.OrderItem, OrderItem>()
            .ReverseMap();
        CreateMap<Models.Product, Product>()
            //.ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))
            //.ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
            //.ForMember(dest => dest.Quantity, src => src.MapFrom(x => x.Quantity))
            .ForMember(dest => dest.Price, src => src.MapFrom(x => new Money(x.Currency, x.Price)))
            .ReverseMap()
            .ForMember(dest => dest.Currency, src => src.MapFrom(x => x.Price.Currency))
            .ForMember(dest => dest.Price, src => src.MapFrom(x => x.Price.Cost));
    }
}
