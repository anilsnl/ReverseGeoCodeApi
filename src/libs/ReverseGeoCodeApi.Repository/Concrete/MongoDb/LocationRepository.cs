using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using ReverseGeoCodeApi.Repository.Abstract;
using ReverseGeoCodeApi.Repository.Models;
using ReverseGeoCodeApi.Repository.Registerer;

namespace ReverseGeoCodeApi.Repository.Concrete.MongoDb
{
    /// <summary>
    /// MongoDb implementation of <see cref="ILocationRepository"/>.
    /// <permission cref="Location"></permission>
    /// </summary>
    public class LocationRepository : ILocationRepository
    {
        private IMongoCollection<Location> _collection;

        /// <summary>
        /// Init new instance of <see cref="LocationRepository"/>
        /// </summary>
        /// <param name="options"></param>
        public LocationRepository(IOptions<MongoDbSettings> options)
        {
            var client = new MongoClient(new MongoUrl(options.Value.ConnectionUrl));
            _collection = client.GetDatabase(options.Value.DbName)
                .GetCollection<Location>(options.Value.LocationCollectionName);
        }

        /// <inheritdoc/>
        public Task<Location> GetLocationInByLatitudeAndLongitudeAsync(double latitude, double longitude)
        {
            var geoJson2DGeographicCoordinates = new GeoJson2DGeographicCoordinates(longitude, latitude);
            var geoJsonPoint = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(geoJson2DGeographicCoordinates);
            var filterDefinition = Builders<Location>
                .Filter
                .GeoIntersects(p => p.Geometry, geoJsonPoint);
            var projectionDefinitionBuilder =
                Builders<Location>.Projection;
            var projectionDefinition = projectionDefinitionBuilder
                .Exclude(a => a.Geometry);
            return _collection.Find(filterDefinition)
                .Project<Location>(projectionDefinition)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public Location GetLocationInByLatitudeAndLongitude(double latitude, double longitude)
        {
            var geoJson2DGeographicCoordinates = new GeoJson2DGeographicCoordinates(longitude, latitude);
            var geoJsonPoint = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(geoJson2DGeographicCoordinates);
            var filterDefinition = Builders<Location>
                .Filter
                .GeoIntersects(p => p.Geometry, geoJsonPoint);
            var projectionDefinitionBuilder =
                Builders<Location>.Projection;
            var projectionDefinition = projectionDefinitionBuilder
                .Exclude(a => a.Geometry);
            return _collection.Find(filterDefinition)
                .Project<Location>(projectionDefinition)
                .FirstOrDefault();
        }

        /// <inheritdoc />
        public Task<string> EnsureIndexCreatedAsync()
        {
            var keys = Builders<Location>.IndexKeys.Geo2DSphere(z => z.Geometry);
            var model = new CreateIndexModel<Location>(keys);
            return _collection.Indexes.CreateOneAsync(model);
        }
    }
}