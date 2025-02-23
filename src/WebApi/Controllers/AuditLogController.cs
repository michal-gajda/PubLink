namespace PubLink.WebApi.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using PubLink.Application.AuditLogs.Queries;
using PubLink.Application.AuditLogs.QueryResults;

[ApiController, Route("[controller]")]
public sealed class AuditLogController(ILogger<AuditLogController> logger, IMediator mediator) : ControllerBase
{
    [HttpGet(Name = "GetUserActions")]
    public async Task<IEnumerable<AuditLog>> GetAsync([FromQuery] Guid organizationId, [FromQuery] int page = 0, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetUserActions for {OrganizationId} ({Page})", organizationId, page);

        var result = await mediator.Send(new GetUserActions { OrganizationId = organizationId, Page = page }, cancellationToken);

        return result;
    }
}
