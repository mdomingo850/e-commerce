using SharedKernel.Domain.Entities.Primitives;

namespace Modules.Orders.Domain.Entities;

public sealed class Customer : AggregateRoot
{
    public string Name { get; set; } = string.Empty;

    private Customer(Guid id, string name) : base(id)
    {
        Id = id;
        Name = name;
    }

    public static Customer Create(Guid id, string name)
    {
        return new Customer(id, name);
    }
}
