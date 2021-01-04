using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using ReverseGeoCode.GrpcService.Abstract;
using ReverseGeoCodeApi.Repository.Abstract;

namespace ReverseGeoCode.GrpcService.Services
{
    /// <summary>
    /// Manages the geo coding operations.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GeoCodingService : Abstract.GeoCodingService.GeoCodingServiceBase
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Init new instance of <see cref="GeoCodingService"/>
        /// </summary>
        /// <param name="locationRepository"></param>
        /// <param name="mapper"></param>
        public GeoCodingService(ILocationRepository locationRepository, IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Reverse geo code.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<ReverseReply> ReverseGeoCode(ReverseRequest request, ServerCallContext context)
        {
            var data = await _locationRepository.GetLocationInByLatitudeAndLongitudeAsync(request.Latitude,
                request.Longitude);
            return data==null ? null : _mapper.Map<ReverseReply>(data);
        }
    }
}