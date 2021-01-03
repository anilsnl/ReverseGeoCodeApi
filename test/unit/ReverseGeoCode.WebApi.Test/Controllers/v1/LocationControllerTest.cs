using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ReverseGeoCode.Models.Location;
using ReverseGeoCode.WebApi.Application.v1.Locations.Queries.GetByLatitudeAndLongitude;
using ReverseGeoCode.WebApi.Controllers.v1;
using Xunit;


namespace ReverseGeoCode.WebApi.Test.Controllers.v1
{
    /// <summary>
    /// Tests <see cref="LocationsController"/>
    /// </summary>
    public class LocationControllerTest
    {
        private readonly Mock<IMediator> _mediatorMock;

        /// <summary>
        /// Init new instance of <see cref="LocationControllerTest"/>
        /// </summary>
        public LocationControllerTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        /// <summary>
        /// Tests whether it returns 200Ok if location found.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(42.32233, 38.211423)]
        [InlineData(30.32233, 28.211423)]
        public async Task GetByLatitudeAndLongitude_Returns200OkWithOvject_IfLocationFound(double latitude,
            double longitude)
        {
            //Arrange
            var request = new Request()
            {
                Latitude = latitude,
                Longitude = longitude
            };
            _mediatorMock.Setup(a => a.Send(It.IsAny<Request>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new Location());
            var controller = new LocationsController(_mediatorMock.Object);
            
            //Act
            var result = await controller.Get(request);
            
            //Assert
            Assert.NotNull(result);
            var okObject = Assert.IsType<OkObjectResult>(result);
            var response =  Assert.IsType<Location>(okObject.Value);
            Assert.NotNull(response);
        }
        
        /// <summary>
        /// Tests whether it returns 404NotFound if location could not be found.
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        [Theory]
        [InlineData(42.32233, 38.211423)]
        [InlineData(30.32233, 28.211423)]
        public async Task GetByLatitudeAndLongitude_Returns404NotFound_IfLocationNotFound(double latitude,
            double longitude)
        {
            //Arrange
            var request = new Request()
            {
                Latitude = latitude,
                Longitude = longitude
            };
            _mediatorMock.Setup(a => a.Send(It.IsAny<Request>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => null);
            var controller = new LocationsController(_mediatorMock.Object);
            
            //Act
            var result = await controller.Get(request);
            
            //Assert
            Assert.NotNull(result); 
            Assert.IsType<NotFoundResult>(result);
        }
    }
}