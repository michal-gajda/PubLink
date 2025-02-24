namespace PubLink.Application.FunctionalTests;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PubLink.Infrastructure;

public abstract class BaseFunctionalTests
{
    protected readonly ServiceProvider serviceProvider;

    protected BaseFunctionalTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<BaseFunctionalTests>()
            .Build();

        var services = new ServiceCollection();

        services.AddLogging();

        services.AddApplication();
        services.AddInfrastructure(configuration);

        this.serviceProvider = services.BuildServiceProvider();
    }
}
