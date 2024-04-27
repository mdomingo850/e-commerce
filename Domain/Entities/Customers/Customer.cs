namespace Domain.Entities.Customers;

public sealed class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    private Customer(int id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }

    public static Customer Create(int id, string firstName, string lastName)
    {
        return new Customer(id, firstName, lastName);
    }
}
