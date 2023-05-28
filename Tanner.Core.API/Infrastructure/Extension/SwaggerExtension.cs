using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Tanner.Core.API.Infrastructure.Configurations;
using Tanner.Core.API.Middleware;

namespace Tanner.Core.API.Infrastructure.Extension
{
    internal static class SwaggerExtension
    {
        internal static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IConfiguration Configuration)
        {
            var swaggerSection = Configuration.GetSection(nameof(SwaggerConfiguration));
            services.Configure<SwaggerConfiguration>(swaggerSection);

            IServiceProvider provider = services.BuildServiceProvider();

            SwaggerConfiguration swaggerConfig = GetSwaggerConfiguration(provider);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerConfig.Version, swaggerConfig.GetOpenApiInfo());
                // XML Documentation
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            return services;
        }
        internal static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            IServiceProvider provider = app.ApplicationServices;
            app.UseDefaultFiles();
            SwaggerConfiguration swaggerConfig = GetSwaggerConfiguration(provider);
            app.UseStaticFiles();

            app.UseSwagger();

            app.UseSwaggerAuthorized();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{swaggerConfig.EndpointUrl}", swaggerConfig.EndpointDescription);
            });

            return app;
        }

        private static SwaggerConfiguration GetSwaggerConfiguration(IServiceProvider provider)
        {
            var swaggerConfigurationOption = provider.GetRequiredService<IOptions<SwaggerConfiguration>>();
            SwaggerConfiguration result = swaggerConfigurationOption.Value;
            return result;
        }
    }
    /// <summary>
    /// Class that represent the authorize extension for swagger
    /// </summary>
    public static class SwaggerAuthorizeExtensions
    {
        /// <summary>
        /// Use the swagger authorized
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();
        }
    }
}
