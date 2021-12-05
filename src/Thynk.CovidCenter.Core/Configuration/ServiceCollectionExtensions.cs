using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Thynk.CovidCenter.Core.Concretes;
using Thynk.CovidCenter.Core.Helpers;
using Thynk.CovidCenter.Core.Interface;
using Thynk.CovidCenter.Repository.Cache;
using Thynk.CovidCenter.Repository.Commands.Implementation;
using Thynk.CovidCenter.Repository.Commands.Interfaces;
using Thynk.CovidCenter.Repository.Queries.Implementation;
using Thynk.CovidCenter.Repository.Queries.Interfaces;

namespace Thynk.CovidCenter.Core.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddScoped<IAvailableDatesService, AvailableDatesService>()
                .AddScoped<IBookingService, BookingService>()
                .AddScoped<IUtilities, Utilities>()
                .AddScoped<ILocationService, LocationService>()
                .AddScoped<IPasswordService, PasswordService>()
                .AddScoped<IReportService, ReportService>()
                .AddScoped<ICache, Cache>()
                .AddScoped(typeof(IDBCommandRepository<>), typeof(DBCommandRepository<>))
                .AddScoped(typeof(IDBQueryRepository<>), typeof(DBQueryRepository<>));

            return services;
        }

    }
}
