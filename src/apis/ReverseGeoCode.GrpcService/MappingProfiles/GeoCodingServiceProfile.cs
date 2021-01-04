using AutoMapper.Configuration;
using ReverseGeoCode.GrpcService.Abstract;
using ReverseGeoCodeApi.Repository.Concrete.MongoDb;

namespace ReverseGeoCode.GrpcService.MappingProfiles
{
    /// <summary>
    /// Use for mapping <see cref="GeoCodingService"/> request and reply models.
    /// </summary>
    public class GeoCodingServiceProfile : MapperConfigurationExpression
    {
        /// <summary>
        /// Creates new instance of <see cref="GeoCodingServiceProfile"/>.
        /// </summary>
        public GeoCodingServiceProfile()
        {
            CreateMap<ReverseReply, LocationRepository>().ReverseMap();
        }
    }
}