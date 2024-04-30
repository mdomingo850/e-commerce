namespace Persistence.Models;

internal sealed record OutboxMessage(Guid Id, string Name, string Content, DateTime CreatedOn, DateTime? ProcessedOn, string? Error);
