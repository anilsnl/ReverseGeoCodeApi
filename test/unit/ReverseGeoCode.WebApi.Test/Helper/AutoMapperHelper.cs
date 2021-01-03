using System.Linq;
using AutoMapper;

namespace ReverseGeoCode.WebApi.Test.Helper
{
    /// <summary>
    /// Used to add AutoMapper profiles of classes that implements <see cref="IMapperConfigurationExpression"/>.
    /// </summary>
    public static class AutoMapperHelper
    {
        /// <summary>
        /// Adds AutoMapper profiles of classes that implements <see cref="IMapperConfigurationExpression"/>.
        /// </summary>
        /// <returns><see cref="AutoMapper.IMapper"/></returns>
        public static IMapper GetAppMapper()
        {
            var assembly = typeof(Startup).Assembly;
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var typeInfo in assembly.DefinedTypes.Where(a =>
                    a.IsClass && a.ImplementedInterfaces.Any(it => it == typeof(IMapperConfigurationExpression))))
                {
                    cfg.AddProfile(typeInfo.AsType());
                }
            });
            return config.CreateMapper();
        }
    }
}