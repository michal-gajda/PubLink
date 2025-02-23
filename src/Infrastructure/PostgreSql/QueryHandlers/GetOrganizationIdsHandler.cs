namespace PubLink.Infrastructure.PostgreSQL.QueryHandlers;

using MediatR;
using Microsoft.Extensions.Logging;
using PubLink.Application.Organizations.Queries;
using PubLink.Infrastructure.PostgreSql.Interfaces;

internal sealed partial class GetOrganizationIdsHandler(ILogger<GetUserActionsHandler> logger, IReadService readService) : IRequestHandler<GetOrganizationIds, IEnumerable<Guid>>
{
    public async Task<IEnumerable<Guid>> Handle(GetOrganizationIds request, CancellationToken cancellationToken)
    {
        this.LogRequest();

        return await readService.GetOrganizationIds();
    }

    [LoggerMessage(LogLevel.Information, Message = "GetOrganizationIds")]
    partial void LogRequest();
}
