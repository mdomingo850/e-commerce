namespace SharedKernel.Domain.Entities.Primitives;

public abstract class Entity
{
    public Guid Id { get; init; }

    protected Entity(Guid id)
    {
        Id = id;
    }
}
