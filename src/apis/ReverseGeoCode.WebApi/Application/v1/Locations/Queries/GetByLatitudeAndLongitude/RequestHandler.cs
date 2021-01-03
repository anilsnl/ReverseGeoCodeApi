using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ReverseGeoCode.Models.Location;
using ReverseGeoCodeApi.Repository.Abstract;

namespace ReverseGeoCode.WebApi.Application.v1.Locations.Queries.GetByLatitudeAndLongitude
{
    /// <summary>
    /// Use to handle request.
    /// <seealso cref="IRequestHandler{TRequest,TResponse}"/>
    /// </summary>
    public class RequestHandler : IRequestHandler<Request, Location>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates new instance of <see cref="RequestHandler"/>
        /// </summary>
        /// <param name="locationRepository"></param>
        /// <param name="mapper"></param>
        public RequestHandler(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Determines user location by using latitude and longitude.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Location> Handle(Request request, CancellationToken cancellationToken)
        {
            await _locationRepository.EnsureIndexCreatedAsync();
            var result =
                await _locationRepository.GetLocationInByLatitudeAndLongitudeAsync(request.Latitude, request.Longitude);
            return result == null ? null : _mapper.Map<Location>(result);
        }
    }
}