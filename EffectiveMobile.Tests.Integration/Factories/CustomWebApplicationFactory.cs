using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EffectiveMobile.Common.EntityModel.Sqlite;

namespace EffectiveMobile.Tests.Integration.Factories;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services
                .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<EffectiveMobileContext>));

            if (descriptor != null) services.Remove(descriptor);
            services.AddDbContext<EffectiveMobileContext>(options =>
            {
                var sp = services.BuildServiceProvider();
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("TestConnection");
                options.UseSqlite(connectionString);
            });
        });
    }
}