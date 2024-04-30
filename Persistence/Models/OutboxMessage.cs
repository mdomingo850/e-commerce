namespace Persistence.Models;

public sealed class OutboxMessage
{
    public OutboxMessage(Guid id, string name, string content, DateTime createdOn, DateTime? processedOn, string? error)
    {
        Id = id;
        Name = name;
        Content = content;
        CreatedOn = createdOn;
        ProcessedOn = processedOn;
        Error = error;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ProcessedOn { get; set; }
    public string? Error { get; set; }

}
