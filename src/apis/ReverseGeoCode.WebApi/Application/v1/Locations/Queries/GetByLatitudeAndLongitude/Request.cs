using MediatR;
using ReverseGeoCode.Models.Location;

namespace ReverseGeoCode.WebApi.Application.v1.Locations.Queries.GetByLatitudeAndLongitude
{
    /// <summary>
    /// The request model.
    /// </summary>
    public class Request : IRequest<Location>
    {
        /// <value>The latitude value.</value>
        public double Latitude { get; set; }

        /// <value>The longitude value.</value>
        public double Longitude { get; set; }
    }
}