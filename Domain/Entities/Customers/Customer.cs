using Domain.Primitives;

namespace Domain.Entities.Customers;

public sealed class Customer : AggregateRoot
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    private Customer(Guid id, string firstName, string lastName) : base(id)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public static Customer Create(Guid id, string firstName, string lastName)
    {
        return new Customer(id, firstName, lastName);
    }
}
