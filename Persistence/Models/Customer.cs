using System.Runtime.InteropServices;

namespace Persistence.Models;

internal class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    private Customer(int Id, string FirstName, string LastName) 
    {
        this.Id = Id;
        this.FirstName = FirstName;
        this.LastName = LastName;
    }

    public static Customer Create(int id, string firstName, string lastName)
    {
        return new Customer(id, firstName, lastName);
    }
}
