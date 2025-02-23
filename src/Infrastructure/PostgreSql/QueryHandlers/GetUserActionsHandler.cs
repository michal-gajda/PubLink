namespace PubLink.Infrastructure.PostgreSQL.QueryHandlers;

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PubLink.Application.AuditLogs.Queries;
using PubLink.Application.AuditLogs.QueryResults;
using PubLink.Infrastructure.PostgreSql.Interfaces;

internal sealed partial class GetUserActionsHandler(ILogger<GetUserActionsHandler> logger, IMapper mapper, IReadService readService) : IRequestHandler<GetUserActions, IEnumerable<AuditLog>>
{
    public async Task<IEnumerable<AuditLog>> Handle(GetUserActions request, CancellationToken cancellationToken)
    {
        this.LogRequest(request.OrganizationId);

        var source = await readService.GetAuditLogs(request.OrganizationId, request.Page, pageSize: 10);

        return mapper.Map<IEnumerable<AuditLog>>(source);
    }

    [LoggerMessage(LogLevel.Information, Message = "GetUserActions for {organizationId}")]
    partial void LogRequest(Guid organizationId);
}
