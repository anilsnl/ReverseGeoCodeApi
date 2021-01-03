using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReverseGeoCodeApi.Repository.Abstract;
using ReverseGeoCodeApi.Repository.Concrete.MongoDb;
using ReverseGeoCodeApi.Repository.Models;

namespace ReverseGeoCodeApi.Repository.Registerer
{
    /// <summary>
    /// This class helps to register repository for MongoDb
    /// </summary>
    public static class MongoServiceRegisterer
    {
        /// <summary>
        /// Configures repository services.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureRepositoryForMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDb"));
            services.AddSingleton<ILocationRepository, LocationRepository>();
        }
    }

    /// <summary>
    /// Use to map mongo db configuration.
    /// </summary>
    public class MongoDbSettings
    {
        /// <value>Mongo connection url.</value>
        public string ConnectionUrl { get; set; }
        
        /// <value>Mongo db name.</value>
        public string DbName { get; set; }
      
        /// <value>Mongo collection name that represent <see cref="Location"/>.</value>
        public string LocationCollectionName { get; set; }
    }
}