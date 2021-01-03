using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace ReverseGeoCode.WebApi.Extensions.Startup
{
    /// <summary>
    /// Configures app versioning.
    /// </summary>
    public static class AppVersioningExtension
    {
        /// <summary>
        /// Register versioning configurations.
        /// </summary>
        /// <param name="services"></param>
        public static void AddAppVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new MediaTypeApiVersionReader();
                options.ApiVersionSelector = new CurrentImplementationApiVersionSelector(options);
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.UseApiBehavior = false;
            });
        }
    }
}