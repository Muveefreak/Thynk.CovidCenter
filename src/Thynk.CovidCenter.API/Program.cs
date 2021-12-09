using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Thynk.CovidCenter.Repository;

namespace Thynk.CovidCenter.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            // we want to migrate database in async thread
            // "await" specially is not used
            _ = Task.Factory.StartNew(() => MigrateDatabaseSafe(host), TaskCreationOptions.LongRunning);

            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static void MigrateDatabaseSafe(IWebHost host)
        {
            try
            {
                using var scope = host.Services.CreateScope();

                var context = scope.ServiceProvider.GetRequiredService<CovidCenterDbContext>();

                //context.Database.EnsureDeleted();
                //try
                //{
                //    if (context.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                //        context.Database.Migrate();
                //}
                //catch(Exception ex)
                //{}
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex);
            }
        }
    }
}
