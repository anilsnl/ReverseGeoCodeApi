using AutoMapper.Configuration;
using ReverseGeoCodeApi.Repository.Models;

namespace ReverseGeoCode.WebApi.MapperProfile
{
    /// <summary>
    /// Mapper for Location base model.
    /// </summary>
    public class LocationMapperProfile : MapperConfigurationExpression
    {
        /// <summary>
        /// Init new instance of <see cref="LocationMapperProfile"/>
        /// </summary>
        public LocationMapperProfile()
        {
            CreateMap<Location, Models.Location.Location>()
                .ReverseMap();
        }
    }
}