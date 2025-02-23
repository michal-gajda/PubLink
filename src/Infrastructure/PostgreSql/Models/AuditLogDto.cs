namespace PubLink.Infrastructure.PostgreSql.Models;

using PubLink.Infrastructure.PostgreSql.Enums;

internal sealed class AuditLogDto
{
    public string? ContractNumber { get; init; } = null;
    public required DateTime CreatedDate { get; init; }
    public TimeSpan Duration { get; init; } = TimeSpan.Zero;
    public required int? NumberOfEntitiesChanged { get; init; }
    public required Type Type { get; init; }
    public required string UserEmail { get; init; }
}
