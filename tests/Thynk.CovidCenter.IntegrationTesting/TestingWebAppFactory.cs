using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Thynk.CovidCenter.API;
using Thynk.CovidCenter.Repository;
using Xunit;

namespace Thynk.CovidCenter.IntegrationTesting
{
    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        public CovidCenterDbContext covidCenterDb { get; set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<CovidCenterDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                services.AddDbContext<CovidCenterDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryCovidCenterTest");
                });
                var sp = services.BuildServiceProvider();
                var scope = sp.CreateScope();
                var appContext = scope.ServiceProvider.GetRequiredService<CovidCenterDbContext>();
                try
                {
                    covidCenterDb = appContext;
                    appContext.Database.EnsureCreated();
                    DatabaseSeed.SeedData(appContext);
                }
                catch (Exception ex)
                {
                    throw;
                }
            });
        }
    }
}
