using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Thynk.CovidCenter.Core.Concretes;
using Thynk.CovidCenter.Core.Interface;

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
                .AddScoped<ILocationService, LocationService>()
                .AddScoped<IPasswordService, PasswordService>()
                .AddScoped<IReportService, ReportService>();

            return services;
        }

    }
}
