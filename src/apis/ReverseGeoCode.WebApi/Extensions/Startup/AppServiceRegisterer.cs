using System.Reflection;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReverseGeoCodeApi.Repository.Registerer;

namespace ReverseGeoCode.WebApi.Extensions.Startup
{
    /// <summary>
    /// Manages app services.
    /// </summary>
    public static class AppServiceRegisterer
    {
        /// <summary>
        /// Registers api services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddAppService(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureRepositoryForMongoDb(configuration);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}