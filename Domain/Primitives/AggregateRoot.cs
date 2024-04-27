namespace Domain.Primitives;

public abstract class AggregateRoot : Entity
{
    protected AggregateRoot(int Id) : base(Id)
    {
    }
}
