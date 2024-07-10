using System.ComponentModel.DataAnnotations;

namespace SharedKernel.Persistence;

public sealed class InboxMessage
{
    public InboxMessage(
        Guid id,
        string name,
        string content,
        DateTime createdOn,
        DateTime? processedOn,
        string? error)
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
    public bool IsLocked { get; set; }
    [Timestamp]
    public byte[] Version { get; private set; }

}
