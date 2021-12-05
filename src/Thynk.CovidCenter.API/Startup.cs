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
using Thynk.CovidCenter.Repository;

namespace Thynk.CovidCenter.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddDbContext<CovidCenterDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(CovidCenterDbContext).Assembly.FullName)));

            //services.AddScoped<IApplicationDbContext>(provider => provider.GetService<CovidCenterDbContext>());

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

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Thynk.CovidCenter.API", Version = "v1" });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Thynk.CovidCenter.API v1"));
            }
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
