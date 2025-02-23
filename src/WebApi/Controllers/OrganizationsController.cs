namespace PubLink.WebApi.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using PubLink.Application.Organizations.Queries;

[ApiController, Route("[controller]")]
public sealed class OrganizationsController(IMediator mediator) : ControllerBase
{
    [HttpGet(Name = "GetOrganizationIds")]
    public async Task<IEnumerable<Guid>> GetAsync([FromQuery] Guid organizationId, [FromQuery] int page = default, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new GetOrganizationIds(), cancellationToken);

        return result;
    }
}
