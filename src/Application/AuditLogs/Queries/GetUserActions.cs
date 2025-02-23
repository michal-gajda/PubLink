namespace PubLink.Application.AuditLogs.Queries;

using MediatR;
using PubLink.Application.AuditLogs.QueryResults;

public sealed record class GetUserActions : IRequest<IEnumerable<AuditLog>>
{
    public required Guid OrganizationId { get; init; }
    public int Page { get; init; } = default;
}
