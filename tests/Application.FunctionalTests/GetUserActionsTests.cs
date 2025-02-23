namespace PubLink.Application.FunctionalTests;

using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PubLink.Application.AuditLogs.Queries;

[TestClass]
public sealed class GetUserActionsTests : BaseFunctionalTests
{
    private readonly IMediator mediator;

    public GetUserActionsTests()
    {
        this.mediator = this.serviceProvider.GetRequiredService<IMediator>();
    }

    [TestMethod]
    public async Task GetUserActionsTests_Should_Work()
    {
        // Given
        var query = new GetUserActions
        {
            OrganizationId = new Guid("e1cd1118-9795-4937-8e94-1822cae3e78f"),
        };

        // When
        var sut = await this.mediator.Send(query);

        // Then
        sut.Should()
            .NotBeNull()
            .And
            .NotBeEmpty()
            ;

        sut.Should()
            .HaveCount(10)
            ;
    }
}
