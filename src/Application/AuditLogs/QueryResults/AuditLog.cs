namespace PubLink.Application.AuditLogs.QueryResults;

public sealed record class AuditLog
{
    public string? ContractNumber { get; init; } = null;
    public required TimeSpan Duration { get; init; }
    public required int NumberOfEntitiesChanged { get; init; }
    public required DateTime StartAt { get; init; }
    public required string Type { get; init; }
    public required string UserEmail { get; init; }
}
