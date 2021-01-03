using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using Moq;
using ReverseGeoCode.WebApi.Application.v1.Locations.Queries.GetByLatitudeAndLongitude;
using ReverseGeoCode.WebApi.Test.Helper;
using ReverseGeoCodeApi.Repository.Abstract;
using Xunit;

namespace ReverseGeoCode.WebApi.Test.Application.v1.Location.Queries.GetByLatitudeAndLongitude
{
    /// <summary>
    /// Tests <see cref="RequestHandler"/>
    /// </summary>
    public class RequestHandlerTest
    {
        private readonly Mock<ILocationRepository> _locationRepositoryMock;

        /// <summary>
        /// Init new instance of <see cref="RequestHandlerTest"/>
        /// </summary>
        public RequestHandlerTest()
        {
            _locationRepositoryMock = new Mock<ILocationRepository>();
        }

        private RequestHandler GetRequestHandler()
        {
            return new RequestHandler(_locationRepositoryMock.Object, AutoMapperHelper.GetAppMapper());
        }

        /// <summary>
        /// Tests whether it returns the location info if the location is determined.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(42.32233, 38.211423)]
        [InlineData(30.32233, 28.211423)]
        public async Task Handle_ReturnsTheFoundLocation_IfLocationDetermined(double latitude, double longitude)
        {
            //Arrange
            var request = new Request()
            {
                Latitude = latitude,
                Longitude = longitude
            };
            _locationRepositoryMock.Setup(a =>
                    a.GetLocationInByLatitudeAndLongitudeAsync(It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(() => new ReverseGeoCodeApi.Repository.Models.Location()
                {
                    City = "Ordu",
                    District = "Perşembe",
                    Id = ObjectId.GenerateNewId(),
                    Neighbourhood = "Kazancili Mh",
                    CityId = 52,
                    DistrictId = 5222,
                    NeighbourhoodId = 5200514
                });
            _locationRepositoryMock.Setup(a => a.EnsureIndexCreatedAsync())
                .ReturnsAsync(() => "test");
            var handler = GetRequestHandler();
            //Act
            var result = await handler.Handle(request, CancellationToken.None);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(52, result.CityId);
            Assert.Equal("Ordu", result.City);
            Assert.NotNull(result.Neighbourhood);
            Assert.Equal(5200514, result.NeighbourhoodId);
            Assert.NotNull(result.District);
            Assert.Equal(5222, result.DistrictId);
        }


        /// <summary>
        /// Tests whether it returns null if the location is not determined.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(42.32233, 38.211423)]
        [InlineData(30.32233, 28.211423)]
        public async Task Handle_ReturnsNull_IfLocationDoesNotDetermined(double latitude, double longitude)
        {
            //Arrange
            var request = new Request()
            {
                Latitude = latitude,
                Longitude = longitude
            };
            _locationRepositoryMock.Setup(a =>
                    a.GetLocationInByLatitudeAndLongitudeAsync(It.IsAny<double>(), It.IsAny<double>()))
                .ReturnsAsync(() => null);
            _locationRepositoryMock.Setup(a => a.EnsureIndexCreatedAsync())
                .ReturnsAsync(() => "test");
            var handler = GetRequestHandler();
            //Act
            var result = await handler.Handle(request, CancellationToken.None);
            //Assert
            Assert.Null(result);
        }
    }
}