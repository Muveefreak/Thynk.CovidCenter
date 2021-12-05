using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Thynk.CovidCenter.Core.Concretes;
using Thynk.CovidCenter.Core.Interface;

namespace Thynk.CovidCenter.Core.Configuration
{
    public static class ServiceCollectionExtensions
    {
        private static Assembly CurrentAssembly => typeof(ServiceCollectionExtensions).Assembly;

        public static void AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
        }

    }
}
