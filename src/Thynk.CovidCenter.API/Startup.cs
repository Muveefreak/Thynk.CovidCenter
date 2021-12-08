using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Converters;
using Thynk.CovidCenter.API.Helpers;
using Thynk.CovidCenter.Core.Configuration;
using Thynk.CovidCenter.Core.Helpers;
using Thynk.CovidCenter.Repository;

namespace Thynk.CovidCenter.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddDbContext<CovidCenterDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(CovidCenterDbContext).Assembly.FullName)));

            services
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.UseCamelCasing(true);
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services
              .AddApiVersioning(options =>
              {
                  options.ApiVersionReader = new UrlSegmentApiVersionReader();
                  options.AssumeDefaultVersionWhenUnspecified = true;
                  options.ReportApiVersions = true;
              })
              .AddVersionedApiExplorer(options =>
              {
                  options.GroupNameFormat = "VVV";
                  options.SubstituteApiVersionInUrl = true;
              });

            services.AddHealthChecks();

            services.AddCore(Configuration);

            services.AddOpenApiDocument(document =>
            {
                document.Title = "Thynk CovidCenter API";
                document.Description = "Thynk CovidCenter API";
                document.DocumentName = "v1";
                document.ApiGroupNames = new[] { "1", "2" };
                document.GenerateEnumMappingDescription = true;
                document.AllowReferencesWithProperties = true;
                document.GenerateKnownTypes = true;
                document.GenerateExamples = true;

                document.UseControllerSummaryAsTagDescription = true;
            });

            services.AddAutoMapper(typeof(AutomapperProfile));
            if (WebHostEnvironment.IsDevelopment())
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddDistributedMemoryCache();
                //services.AddStackExchangeRedisCache(option =>
                //{
                //    option.Configuration = Configuration.GetValue<string>("AppSettings:RedisConfig:Url");
                //    option.InstanceName = "Thynk_CovidCenter_";
                //});
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseHealthChecks("/health");

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthorization();
            app.ConfigureExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
