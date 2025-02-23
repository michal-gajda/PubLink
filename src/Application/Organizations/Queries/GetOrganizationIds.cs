using MediatR;

namespace PubLink.Application.Organizations.Queries;

public sealed record class GetOrganizationIds : IRequest<IEnumerable<Guid>> { }
