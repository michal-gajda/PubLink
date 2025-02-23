namespace PubLink.Infrastructure.PostgreSql.Interfaces;

using PubLink.Infrastructure.PostgreSql.Models;

internal interface IReadService
{
    Task<IEnumerable<AuditLogDto>> GetAuditLogs(Guid correlationId, int page, int pageSize);
    Task<IEnumerable<Guid>> GetOrganizationIds();
}
