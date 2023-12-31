using GymWorkDisclosed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
namespace IntegrationTests;

public class GymWorkDisclosedWebAppFactory : WebApplicationFactory<GymWorkDisclosedProgram>
{
    private const string environment = "Testing";
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(environment);
        builder.UseUrls("http://localhost:5206");
    }
}