namespace PubLink.Application.FunctionalTests;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PubLink.Infrastructure;

public abstract class BaseFunctionalTests
{
    const string USER_SECRETS_ID = "0f23e17d-ea8d-4b5a-98a8-72763297bbf9";
    protected readonly ServiceProvider serviceProvider;

    protected BaseFunctionalTests()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets(USER_SECRETS_ID)
            .Build();

        var services = new ServiceCollection();

        services.AddLogging();

        services.AddApplication();
        services.AddInfrastructure(configuration);

        this.serviceProvider = services.BuildServiceProvider();
    }
}
