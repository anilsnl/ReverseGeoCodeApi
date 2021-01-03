using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ReverseGeoCodeApi.Repository.Abstract;
using ReverseGeoCodeApi.Repository.Concrete.MongoDb;
using ReverseGeoCodeApi.Repository.Models;
using ReverseGeoCodeApi.Repository.Registerer;
using Xunit;

namespace ReverseGeoCode.Repository.IntegrationTest.MongoDb
{
    /// <summary>
    /// Tests <see cref="LocationRepositoryTest"/>
    /// </summary>
    public class LocationRepositoryTest
    {
        #region Definations

        private readonly ILocationRepository _locationRepository;

        /// <summary>
        /// Creates instance of <see cref="LocationRepositoryTest"/>
        /// </summary>
        public LocationRepositoryTest()
        {
            var url =
                "mongodb://root:52@localhost:27017/location?authSource=admin&readPreference=primary&appname=MongoDB%20Compass&ssl=false";
            _locationRepository = new LocationRepository(Options.Create(new MongoDbSettings()
            {
                ConnectionUrl = url,
                DbName = "location",
                LocationCollectionName = "geo_data_1"
            }));
        }

        #endregion
        
        #region GetLocationInByLatitudeAndLongitudeAsync

        
        /// <summary>
        /// Tests whether it returns <see cref="Location"/> if data is found with the passing latitude and longitude.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(41.32132, 28.123212)]
        public async Task GetLocationInByLatitudeAndLongitudeAsync_ReturnsLocation_IfLocationDetermined(double latitude,
            double longitude)
        {
            var result = await _locationRepository.GetLocationInByLatitudeAndLongitudeAsync(latitude, longitude);
            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Geometry);
        }

        /// <summary>
        /// Tests whether it returns null if data is not found with the passing latitude and longitude.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(-41.32132, -28.123212)]
        public async Task GetLocationInByLatitudeAndLongitudeAsync_ReturnsNull_IfLocationDoesNotDetermined(
            double latitude,
            double longitude)
        {
            var result = await _locationRepository.GetLocationInByLatitudeAndLongitudeAsync(latitude, longitude);
            //Assert
            Assert.Null(result);
        }

        
        #endregion

        #region GetLocationInByLatitudeAndLongitude

        /// <summary>
        /// Tests whether it returns <see cref="Location"/> if data is found with the passing latitude and longitude.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        [Theory]
        [InlineData(41.32132, 28.123212)]
        public void GetLocationInByLatitudeAndLongitude_ReturnsLocation_IfLocationDetermined(double latitude,
            double longitude)
        {
            var result = _locationRepository.GetLocationInByLatitudeAndLongitude(latitude, longitude);
            //Assert
            Assert.NotNull(result);
            Assert.Null(result.Geometry);
        }

        /// <summary>
        /// Tests whether it returns null if data is not found with the passing latitude and longitude.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        [Theory]
        [InlineData(-41.32132, -28.123212)]
        public void GetLocationInByLatitudeAndLongitude_ReturnsNull_IfLocationDoesNotDetermined(double latitude,
            double longitude)
        {
            var result = _locationRepository.GetLocationInByLatitudeAndLongitude(latitude, longitude);
            //Assert
            Assert.Null(result);
        }
        
        
        #endregion

        #region CreateIndexAsync

        /// <summary>
        /// Tests whether it returns the created or updated index name if index created or updated successfully.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task EnsureIndexCreatedAsync_ReturnsNameOfTheIndex_IfIndexCreated()
        {
            var result = await _locationRepository.EnsureIndexCreatedAsync();
            
            //Assert
            Assert.NotNull(result);
        }

        #endregion
    }
}