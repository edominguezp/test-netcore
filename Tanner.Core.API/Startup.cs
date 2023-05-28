using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tanner.Core.API.Helpers;
using Tanner.Core.API.Infrastructure;
using Tanner.Core.API.Infrastructure.Configurations;
using Tanner.Core.API.Infrastructure.Extension;
using Tanner.Core.API.Middleware;

namespace Tanner.Core.API
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="environment"></param>
        public Startup(IHostingEnvironment environment)
        {
            string environmentName = environment.EnvironmentName;
            var builder = new ConfigurationBuilder() 
                 .SetBasePath(environment.ContentRootPath)
                 .AddJsonFile("Settings/appsettings.json", optional: true, reloadOnChange: true)
                 .AddJsonFile($"Settings/appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables();

            Configuration = builder.Build();

            Environment = environment;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Environment
        /// </summary>
        public IHostingEnvironment Environment { get; }

        private readonly string AllowSpecificOrigins = nameof(AllowSpecificOrigins);

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddSwaggerDocumentation(Configuration);
            
            services.InitializeAPI(Configuration);


            CorsConfiguration corsConf = services.BuildServiceProvider().GetService<IOptions<CorsConfiguration>>().Value;

            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    //builder.WithOrigins(corsConf.Origins);
                });
            });

            services.AddApplicationInsightsTelemetry(ConfigurationSetting.instance.GetValue("ApplicationInsights:InstrumentationKey"));
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The Sluggish HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHealthChecks("/health");

            app.UseSwaggerDocumentation();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseCors(AllowSpecificOrigins);

            app.UseAuthentication();

            app.UseSwagger();

            app.UseMvc();
        }

    }
      
}
