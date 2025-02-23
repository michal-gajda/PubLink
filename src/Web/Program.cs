namespace PubLink.Web;

internal static class Program
{
    private const int EXIT_SUCCESS = 0;

    public static async Task<int> Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHealthChecks();

        builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

        var app = builder.Build();

        app.UseHealthChecks("/health");

        app.UseDefaultFiles();
        app.UseStaticFiles();

        app.MapReverseProxy();

        await app.RunAsync();

        return EXIT_SUCCESS;
    }
}
