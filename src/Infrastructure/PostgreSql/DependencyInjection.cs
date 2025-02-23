namespace PubLink.Infrastructure.PostgreSql;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PubLink.Infrastructure.PostgreSql.Interfaces;
using PubLink.Infrastructure.PostgreSql.Services;

public static class DependencyInjection
{
    public static void AddPostgreSql(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new PostgreSqlOptions
        {
            ConnectionString = configuration.GetConnectionString("RekrutacjaDb")!,
        };

        services.AddSingleton(options);
        services.AddSingleton<IReadService, ReadService>();

        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }
}
