using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ReverseGeoCode.WebApi.Extensions.Startup
{
    /// <summary>
    /// Used to configure Swagger options.
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Add swagger to app services.
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwagger(this IServiceCollection services)
        {
            var apiName = "ReverseGeoCode WebApi";
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = $"{apiName} v1",
                    Description = $"{apiName} - v1"
                });

                var xmlComments = Path.Combine(AppContext.BaseDirectory, $"{apiName}.xml");
                if (File.Exists(xmlComments)) c.IncludeXmlComments(xmlComments);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer"
                });

                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v}".Contains(docName));
                });

                c.CustomSchemaIds((t) => t.FullName);
            });
        }

        /// <summary>
        /// Adds swagger to app pipeline.
        /// </summary>
        /// <param name="app"></param>
        public static void UseAndConfigureSwagger(this IApplicationBuilder app)
        {
            var projectName = "ReverseGeoCode WebApi";
            app.UseSwagger(c => c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
            {
                if (!httpReq.Headers.TryGetValue("Referer", out var refererValues)) return;

                var requestUri = new Uri(refererValues[0].Replace("/swagger/index.html", string.Empty));

                swaggerDoc.Servers = new List<OpenApiServer>
                {
                    new OpenApiServer
                    {
                        Url = $"{httpReq.Scheme}://{requestUri.Host}:{requestUri.Port}{requestUri.AbsolutePath}"
                    }
                };
            }));

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", $"{projectName} - v1");
                c.DocExpansion(DocExpansion.None);
            });
        }
    }
}